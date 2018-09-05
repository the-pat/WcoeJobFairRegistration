using System;
using System.Linq;
using DYMO.Label.Framework;
using WcoeJobFairRegistration.DataAccess;
using WcoeJobFairRegistration.Models;

namespace WcoeJobFairRegistration.Services
{
    internal class DymoService : IPrintService
    {
        private ILabelWriterPrinter _printer = null;

        /// <summary>
        /// The default constructor. Connects to the first LabelWriterPrinter available.
        /// </summary>
        public DymoService()
        {
            Connect();
        }

        /// <summary>
        /// Given a student, print a label with the student's information
        /// </summary>
        /// <param name="student">The given student</param>
        /// <returns>True represents that the label was printed successfully</returns>
        public bool PrintStudentLabel(AttendingStudent student)
        {
            try
            {
                var label = Label.Open("Labels/student.label");

                label.SetObjectText("name", $"{student.FirstName} {student.LastName}");
                this.Print(label);

                return true;
            }
            catch(Exception ex)
            {
                // TODO: Log error
                //_dataAccess.LogError($"Error printing the student label. {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Given an employer, print a label with the employer's information
        /// </summary>
        /// <param name="employer">The given employer</param>
        /// <returns>True represents that the label was printed successfully</returns>
        public bool PrintEmployerLabel(Employer employer)
        {
            try
            {
                var label = Label.Open("Labels/employer.label");

                // fill out the label information
                label.SetObjectText("NAME", $"{employer.FirstName} {employer.LastName}");
                label.SetObjectText("ORGANIZATION", employer.Organization);
                label.SetObjectText("TITLE", employer.Title);

                this.Print(label);

                return true;
            }
            catch(Exception ex)
            {
                // TODO: Log error
                //_dataAccess.LogError($"Error printing the employer label. {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Given a label, print it
        /// </summary>
        /// <param name="label">The Given label</param>
        private void Print(ILabel label)
        {
            if (!IsConnected() && !Connect()) throw new Exception("Could not connect to a printer.");

            var printJob = this._printer.CreatePrintJob(null);

            printJob.AddLabel(label);
            printJob.Print();
        }

        public bool IsConnected()
        {
            if(_printer == null)
                return false;

            return _printer.IsConnected;
        }

        public bool Connect()
        {
            _printer = new Printers().FirstOrDefault(p => p.IsConnected && p is ILabelWriterPrinter) as ILabelWriterPrinter;
            return _printer != null;
        }
    }
}