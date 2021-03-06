﻿//====================================================================================================
// Base code generated with Relativity: HTTP Service Controller Gen (Build 1.0.5116.29382)
// Layered Architecture Solution Guidance (http://layerguidance.codeplex.com)
//
// Generated by Administrator at MKB-SERENAY-04 on 01/03/2014 16:24:58 
//====================================================================================================

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Web.Http;
using LeaveSample.Business;
using LeaveSample.Entities;
using LeaveSample.Services.Contracts;

namespace LeaveSample.Services.Http
{
    /// <summary>
    /// Leave HTTP service controller.
    /// </summary>
    [RoutePrefix("Dummy")]
    public class DummyService : ApiController
    {
        /// <summary>
        /// Calls the GetLeaveById business method of the LeaveComponent.
        /// </summary>
        /// <param name="leaveID"> A leaveID value.</param>
        /// <returns>Returns a Leave object.</returns>
        [HttpGet]
        //[Route("{action}/{leaveID}")]
        public Leave GetLeaveById(long leaveID)
        {
            LeaveComponent bc = new LeaveComponent();
            return bc.GetLeaveById(leaveID);
        }

        /// <summary>
        /// Calls the ListLeavesByEmployee business method of the LeaveComponent.
        /// </summary>
        /// <param name="maximumRows"> A maximumRows value.</param>
        /// <param name="startRowIndex"> A startRowIndex value.</param>
        /// <param name="sortExpression"> A sortExpression value.</param>
        /// <param name="employee"> A employee value.</param>
        /// <param name="category"> A category value.</param>
        /// <param name="status"> A status value.</param>
        /// <param name="int"> A int value.</param>
        /// <returns>Returns a List<Leave> object.</returns>
        [HttpGet]
        [Route("{action}")]
        //[Route("{action}/{maximumRows}/{startRowIndex}sortExpression={sortExpression}&employee={employee}&category={category=}&status={status=}")]
        public ListLeavesResponse ListLeavesByEmployee(int maximumRows, int startRowIndex,
            string sortExpression, string employee, LeaveCategories? category,
            LeaveStatuses? status)
        {
            int totalRowCount = 0;
            var response = new ListLeavesResponse();

            LeaveComponent bc = new LeaveComponent();
            response.Leaves = bc.ListLeavesByEmployee(maximumRows, startRowIndex, sortExpression,
                employee, category, status, out totalRowCount);
            response.TotalRowCount = totalRowCount;

            return response;
        }

        /// <summary>
        /// Calls the ListLogsByLeave business method of the LeaveComponent.
        /// </summary>
        /// <param name="leaveID"> A leaveID value.</param>
        /// <returns>Returns a List<LeaveStatusLog> object.</returns>
        [HttpGet]
        //[Route("{action}/{leaveID}")]
        public List<LeaveStatusLog> ListLogsByLeave(long leaveID)
        {
            LeaveComponent bc = new LeaveComponent();
            return bc.ListLogsByLeave(leaveID);
        }
    }
}
