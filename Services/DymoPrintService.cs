using System;
using System.Linq;
using DYMO.Label.Framework;
using WcoeJobFairRegistration.Models;

namespace WcoeJobFairRegistration.Services
{
    internal class DymoPrintService : IPrintService
    {
        private readonly ILabelWriterPrinter _printer;

        /// <summary>
        /// The default constructor. Connects to the first LabelWriterPrinter available.
        /// </summary>
        public DymoPrintService()
            : this(Framework.GetLabelWriterPrinters().FirstOrDefault(p => p.IsConnected) as ILabelWriterPrinter)
        {  }

        /// <summary>
        /// A constructor. Connects to the given LabelWriterPrinter.
        /// </summary>
        /// <param name="printer">The given LabelWriterPrinter</param>
        public DymoPrintService(ILabelWriterPrinter printer)
        {
            if (this._printer == null)
            {
                throw new NullReferenceException("Unable to establish connection to printer.");
            }

            this._printer = printer;
        }

        /// <summary>
        /// Given a student, print a label with the student's information
        /// </summary>
        /// <param name="student">The given student</param>
        /// <returns>True represents that the label was printed successfully</returns>
        public bool PrintStudentLabel(Student student)
        {
            try
            {
                var label = Label.Open("Labels/student.label");

                label.SetObjectText("NAME", $"{student.FirstName} {student.LastName}");
                label.SetObjectText("MAJOR", student.Major);
                this.Print(label);

                return true;
            }
            catch (Exception ex)
            {
                // TODO: Log the error
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

                this.Print(label);

                return true;
            }
            catch (Exception ex)
            {
                // TODO: Log the error
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
