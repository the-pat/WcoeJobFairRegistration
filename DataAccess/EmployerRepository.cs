using System;
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
        private static string EmployerFile = "eoc_employers.json";

        private List<Employer> _employers;

        public EmployerRepository()
        {
            _employers = new List<Employer>();
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

        public async Task EnsureInitialized()
        {
            if(!_isInitialized)
            {
                await Initialize();
                _isInitialized = true;
            }
        }

        private async Task Initialize()
        {
            try
            {
                await Task.Run(async () =>
                {
                    using(var reader = new StreamReader(new FileStream(EmployerFile, FileMode.Create)))
                    {
                        var content = await reader.ReadToEndAsync();
                        _employers = JsonConvert.DeserializeObject<List<Employer>>(content);
                    }
                });
            }
            catch(Exception ex)
            {
                Trace.WriteLine("Error initializing Teacher Repository\nMessage: {0}", ex.Message);
            }
        }
    }
}