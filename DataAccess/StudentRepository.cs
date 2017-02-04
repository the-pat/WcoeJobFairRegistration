using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WcoeJobFairRegistration.Models;

namespace WcoeJobFairRegistration.DataAccess
{
    public class StudentRepository : IStudentRepository
    {
        private bool _isInitialized = false;

        private static string JobGridStudentFile = "eoc_job-grid.json";
        private static string AttendingStudentFile = "eoc_attending.json";

        private List<AttendingStudent> _attendingStudents;
        private List<JobGridStudent> _jobGridStudents;

        public StudentRepository()
        {
            _attendingStudents = new List<AttendingStudent>();
            _jobGridStudents = new List<JobGridStudent>();
        }

        public async Task<JobGridStudent> Find(string rNumber)
        {
            await EnsureInitialized();
            return _jobGridStudents.SingleOrDefault(student => student.RNumber == rNumber);
        }

        /// <summary>
        /// Load a more recent job grid data set from a csv file
        /// </summary>
        /// <param name="fileName">The name of the file to load the data from</param>
        public Task<bool> Load(string fileName)
        {
            return Task.Run(async () =>
            {
                try
                {
                    // Parse data from CSV file
                    string content;
                    using(var reader = new StreamReader(new FileStream(fileName, FileMode.Open)))
                    {
                        content = await reader.ReadToEndAsync();
                    }
                    var lines = content.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).Skip(1);
                    _jobGridStudents = lines.Select(line =>
                    {
                        var parts = line.Split(',');
                        return new JobGridStudent
                        {
                            RNumber = parts[5].Trim(),
                            FirstName = parts[1].Trim(),
                            LastName = parts[2].Trim()
                        };
                    }).ToList();
                    _jobGridStudents = _jobGridStudents ?? new List<JobGridStudent>();

                    // Save new data to disk
                    using(var writer = new StreamWriter(new FileStream(JobGridStudentFile, FileMode.Create)))
                    {
                        await writer.WriteAsync(JsonConvert.SerializeObject(_jobGridStudents));
                        return true;
                    }
                }
                catch(Exception ex)
                {
                    Trace.WriteLine("Error initializing Student Repository\nMessage: {0}", ex.Message);
                    return false;
                }
            });
        }

        public Task<bool> Save(AttendingStudent student)
        {
            if(student == null)
                throw new InvalidOperationException("StudentRepository.Save() - student cannot be null");

            return Task.Run(async () =>
            {
                await EnsureInitialized().ConfigureAwait(false);

                try
                {
                    _attendingStudents.Add(student);
                    using(var writer = new StreamWriter(new FileStream(AttendingStudentFile, FileMode.Create)))
                    {
                        await writer.WriteAsync(JsonConvert.SerializeObject(_attendingStudents)).ConfigureAwait(false);
                        return true;
                    }
                }
                catch(Exception ex)
                {
                    Trace.WriteLine("Error Saving Student Data\nMessage: {0}", ex.Message);
                    return false;
                }
            });
        }

        /// <summary>
        /// Ensures student data has been loaded. Must be called before any other method
        /// </summary>
        public Task EnsureInitialized()
        {
            if(!_isInitialized)
            {
                return Initialize();
            }
            return Task.FromResult(false);
        }

        Task Initialize()
        {
            try
            {
                return Task.Run(async () =>
                {
                    // Load data used for searching
                    using(var reader = new StreamReader(new FileStream(JobGridStudentFile, FileMode.Create)))
                    {
                        var content = await reader.ReadToEndAsync();
                        _jobGridStudents = JsonConvert.DeserializeObject<List<JobGridStudent>>(content);
                        _jobGridStudents = _jobGridStudents ?? new List<JobGridStudent>();
                    }

                    // Load persisted version
                    using(var reader = new StreamReader(new FileStream(AttendingStudentFile, FileMode.Create)))
                    {
                        var content = await reader.ReadToEndAsync();
                        _attendingStudents = JsonConvert.DeserializeObject<List<AttendingStudent>>(content);
                        _attendingStudents = _attendingStudents ?? new List<AttendingStudent>();
                    }

                    _isInitialized = true;
                });
            }
            catch(Exception ex)
            {
                Trace.WriteLine("Error initializing Student Repository\nMessage: {0}", ex.Message);
                return Task.FromResult(false);
            }
        }
    }
}