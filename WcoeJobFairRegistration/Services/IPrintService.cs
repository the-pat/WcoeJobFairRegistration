using WcoeJobFairRegistration.Models;

namespace WcoeJobFairRegistration.Services
{
    public interface IPrintService
    {
        /// <summary>
        /// Given a student, print a label with the student's information
        /// </summary>
        /// <param name="student">The given student</param>
        /// <returns>True represents that the label was printed successfully</returns>
        bool PrintStudentLabel(AttendingStudent student);

        /// <summary>
        /// Given an employer, print a label with the employer's information
        /// </summary>
        /// <param name="employer">The given employer</param>
        /// <returns>True represents that the label was printed successfully</returns>
        bool PrintEmployerLabel(Employer employer);

        /// <summary>
        /// Returns true if there is a valid connection to the printer and false otherwise
        /// </summary>
        bool IsConnected();

        /// <summary>
        /// Attempts to connect to the printing service
        /// </summary>
        /// <returns>True if the connection was successful. False otherwise</returns>
        bool Connect();
    }
}