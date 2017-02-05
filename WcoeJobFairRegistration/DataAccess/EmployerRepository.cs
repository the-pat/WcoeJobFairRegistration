using System;
using System.Windows;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WcoeJobFairRegistration.Models;

namespace WcoeJobFairRegistration.DataAccess
{
    public class EmployerRepository : IEmployerRepository
    {
        private bool _isInitialized = false;
        private readonly string EmployerFile = "eoc_employers.json";

        private List<Employer> _employers;

        public EmployerRepository()
        {
            _employers = new List<Employer>();

            EmployerFile = Path.Combine((Application.Current as App).ReportingFolderPath, EmployerFile);
        }

        public Task<bool> Save(Employer employer)
        {
            return Task.Run(async () =>
            {
                await EnsureInitialized().ConfigureAwait(false);

                try
                {
                    _employers.Add(employer);
                    using(var writer = new StreamWriter(new FileStream(EmployerFile, FileMode.Create)))
                    {
                        await writer.WriteAsync(JsonConvert.SerializeObject(_employers)).ConfigureAwait(false);
                        return true;
                    }
                }
                catch(Exception ex)
                {
                    Trace.WriteLine("Error Saving Employer Data\nMessage: {0}", ex.Message);
                    return false;
                }
            });
        }

        public Task EnsureInitialized()
        {
            if (!_isInitialized)
            {
                return Initialize();
            }
            return Task.FromResult(false);
        }

        private Task Initialize()
        {
            try
            {
                return Task.Run(async () =>
                {
                    using(var reader = new StreamReader(new FileStream(EmployerFile, FileMode.OpenOrCreate)))
                    {
                        var content = await reader.ReadToEndAsync();
                        _employers = JsonConvert.DeserializeObject<List<Employer>>(content);
                        _employers = _employers ?? new List<Employer>();
                    }
                });
            }
            catch(Exception ex)
            {
                Trace.WriteLine("Error initializing Teacher Repository\nMessage: {0}", ex.Message);
                return Task.FromResult(false);
            }
        }
    }
}