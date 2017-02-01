using System;
using System.Linq;
using DYMO.Label.Framework;
using WcoeJobFairRegistration.DataAccess;
using WcoeJobFairRegistration.Models;

namespace WcoeJobFairRegistration.Services
{
    internal class DymoService : IPrintService
    {
        private readonly ILabelWriterPrinter _printer;
        private readonly IDataAccess _dataAccess;

        /// <summary>
        /// The default constructor. Connects to the first LabelWriterPrinter available.
        /// </summary>
        public DymoService()
            : this(Framework.GetLabelWriterPrinters().FirstOrDefault(p => p.IsConnected) as ILabelWriterPrinter)
        {  }

        /// <summary>
        /// A constructor. Connects to the given LabelWriterPrinter.
        /// </summary>
        /// <param name="printer">The given LabelWriterPrinter</param>
        public DymoService(ILabelWriterPrinter printer)
        {
            _dataAccess = new DataAccess.DataAccess(new LocalStudentRepository(), new LocalEmployerRepository());

            if (this._printer == null)
            {
                // TODO: Log error
                //_dataAccess.LogError("Unable to establish connection to a printer.");
            }

            this._printer = printer;
        }

        /// <summary>
        /// Given a student, print a label with the student's information
        /// </summary>
        /// <param name="student">The given student</param>
        /// <returns>True represents that the label was printed successfully</returns>
        public bool PrintLabel(Student student)
        {
            try
            {
                var label = Label.Open("Labels/student.label");

                label.SetObjectText("NAME", $"{student.FirstName} {student.LastName}");
                this.Print(label);

                return true;
            }
            catch (Exception ex)
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
        public bool PrintLabel(Employer employer)
        {
            try
            {
                var label = Label.Open("Labels/employer.label");

                // fill out the label information
                label.SetObjectText("NAME", $"{employer.FirstName} {employer.LastName}");
                label.SetObjectText("ORGANIZATION", employer.Organization);

                this.Print(label);

                return true;
            }
            catch (Exception ex)
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
            var printJob = this._printer.CreatePrintJob(null);

            printJob.AddLabel(label);
            printJob.Print();
        }
    }
}
