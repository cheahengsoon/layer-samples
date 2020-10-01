//====================================================================================================
// Base code generated with Motion: BC Gen (Build 2.2.4841.16573)
// Layered Architecture Solution Guidance (http://layerguidance.codeplex.com)
//
// Generated by Serena Yeoh at ALIENWARE on 05/08/2013 14:18:27 
//====================================================================================================

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LeaveSample.Entities;
using LeaveSample.Data;
using System.Transactions;
using System.Linq;
using LeaveSample.Framework;

// NOTE:
// 
// Business Components are used to contain the business processing logic of the
// application. Business components interact with data access components to
// store and retrieved data that is required for processing. Business components
// can also re-validate data that is passed in from the Presentation or Services
// layer.
//
// In this sample, you can see that each business method is coded to perform a 
// specific task. Individually, they appear to be independent from each other 
// but collectively, they will be used to assemble the business processing flow
// through the Workflow Component.

namespace LeaveSample.Business
{
    /// <summary>
    /// Leave business component.
    /// </summary>
    public partial class LeaveComponent
    {
        /// <summary>
        /// Apply business method. 
        /// </summary>
        /// <param name="leave">A leave value.</param>
        /// <returns>Returns a Leave object.</returns>
        public Leave Apply(Leave leave)
        {
            leave.Status = LeaveStatuses.Pending;
            leave.DateSubmitted = DateTime.Now;
            leave.IsCompleted = false;

            LeaveStatusLog log = CreateLog(leave);

            // Data access component declarations.
            var leaveDAC = new LeaveDAC();
            var leaveStatusLogDAC = new LeaveStatusLogDAC();

            Validations.ValidateLeaveDates(leave);

            // Check for overlapping leaves.
            if (leaveDAC.IsOverlap(leave))
            {
                throw new ApplicationException("Date range is overlapping with another leave.");
            }

            using (TransactionScope ts =
                new TransactionScope(TransactionScopeOption.Required))
            {
                // Step 1 - Calling Create on LeaveDAC.
                leaveDAC.Create(leave);

                // Step 2 - Calling Create on LeaveStatusLogDAC.
                log.LeaveID = leave.LeaveID;
                leaveStatusLogDAC.Create(log);

                ts.Complete();
            }

            return leave;
        }

        /// <summary>
        /// Cancel business method. 
        /// </summary>
        /// <param name="leave">A leave value.</param>
        /// <returns>Returns a Leave object.</returns>
        public Leave Cancel(Leave leave)
        {
            leave.Status = LeaveStatuses.Cancelled;
            leave.IsCompleted = true;

            UpdateStatus(leave);

            return leave;
        }

        /// <summary>
        /// Approve business method. 
        /// </summary>
        /// <param name="leave">A leave value.</param>
        /// <returns>Returns a Leave object.</returns>
        public Leave Approve(Leave leave)
        {
            leave.Status = LeaveStatuses.Approved;
            leave.IsCompleted = true;

            UpdateStatus(leave);

            return leave;
        }

        /// <summary>
        /// Reject business method. 
        /// </summary>
        /// <param name="leave">A leave value.</param>
        /// <returns>Returns a Leave object.</returns>
        public Leave Reject(Leave leave)
        {
            leave.Status = LeaveStatuses.Rejected;
            leave.IsCompleted = true;

            UpdateStatus(leave);

            return leave;
        }

        private static LeaveStatusLog CreateLog(Leave leave)
        {
            LeaveStatusLog log = new LeaveStatusLog();
            log.Date = DateTime.Now;
            log.LeaveID = leave.LeaveID;
            log.Status = leave.Status;
            return log;
        }

        private void UpdateStatus(Leave leave)
        {
            LeaveStatusLog log = CreateLog(leave);

            // Data access component declarations.
            var leaveDAC = new LeaveDAC();
            var leaveStatusLogDAC = new LeaveStatusLogDAC();

            using (TransactionScope ts =
                new TransactionScope(TransactionScopeOption.Required))
            {
                // Step 1 - Calling UpdateById on LeaveDAC.
                leaveDAC.UpdateStatus(leave);

                // Step 2 - Calling Create on LeaveStatusLogDAC.
                leaveStatusLogDAC.Create(log);

                ts.Complete();
            }
        }

        /// <summary>
        /// ListLeavesByEmployee business method. 
        /// </summary>
        /// <param name="startRowIndex">A startRowIndex value.</param>
        /// <param name="maximumRows">A maximumRows value.</param>
        /// <param name="sortExpression">A sortExpression value.</param>
        /// <param name="employee">A employee value.</param>
        /// <param name="category">A category value.</param>
        /// <param name="status">A status value.</param>
        /// <returns>Returns a List<Leave> object.</returns>
        public List<Leave> ListLeavesByEmployee(int maximumRows, int startRowIndex, 
            string sortExpression, string employee, LeaveCategories? category, LeaveStatuses? status,
            out int totalRowCount)
        {
            List<Leave> result = default(List<Leave>);

            if (string.IsNullOrWhiteSpace(sortExpression))
                sortExpression = "DateSubmitted DESC";

            // Data access component declarations.
            var leaveDAC = new LeaveDAC();

            // Step 1 - Calling Select on LeaveDAC.
            result = leaveDAC.Select(maximumRows, startRowIndex, sortExpression,
                employee, category, status);

            // Step 2 - Get count.
            totalRowCount = leaveDAC.Count(employee, category, status);

            return result;

        }

        /// <summary>
        /// ListLogsByLeave business method. 
        /// </summary>
        /// <param name="leaveID">A leaveID value.</param>
        /// <returns>Returns a List<LeaveStatusLog> object.</returns>
        public List<LeaveStatusLog> ListLogsByLeave(long leaveID)
        {
            // Data access component declarations.
            LeaveStatusLogDAC leaveStatusLogDAC = new LeaveStatusLogDAC();

            // Step 1 - Calling SelectByLeave on LeaveStatusLogDAC.
            List<LeaveStatusLog> result = leaveStatusLogDAC.SelectByLeave(leaveID);
            return result;

        }


        /// <summary>
        /// GetLeaveById business method. 
        /// </summary>
        /// <param name="leaveID">A leaveID value.</param>
        /// <returns>Returns a Leave object.</returns>
        public Leave GetLeaveById(long leaveID)
        {
            Leave result = default(Leave);

            // Data access component declarations.
            var leaveDAC = new LeaveDAC();

            // Step 1 - Calling SelectById on LeaveDAC.
            result = leaveDAC.SelectById(leaveID);
            return result;

        }


        
    }
}