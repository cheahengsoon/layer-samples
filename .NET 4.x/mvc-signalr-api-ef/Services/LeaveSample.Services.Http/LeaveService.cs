﻿//====================================================================================================
// Base code generated with Relativity: HTTP Service Controller Gen (Build 1.0.5116.29382)
// Layered Architecture Solution Guidance (http://layerguidance.codeplex.com)
//
// Generated by Serena Yeoh at ALIENWARE on 01/03/2014 16:24:58 
//====================================================================================================

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Web.Http;
using LeaveSample.Business;
using LeaveSample.Entities;
using LeaveSample.Services.Contracts;
using System.Net;

namespace LeaveSample.Services.Http
{
    /// <summary>
    /// Leave HTTP service controller.
    /// </summary>
    [RoutePrefix("Leave")]
    public class LeaveService : ApiController
    {
        /// <summary>
        /// Calls the GetLeaveById business method of the LeaveComponent.
        /// </summary>
        /// <param name="leaveID"> A leaveID value.</param>
        /// <returns>Returns a Leave object.</returns>
        [HttpGet]
        [Route("GetLeaveById/{leaveID}")]
        public Leave GetLeaveById(long leaveID)
        {
            Leave result = null;

            try
            {
                LeaveComponent bc = new LeaveComponent();
                result = bc.GetLeaveById(leaveID);
            }
            catch(Exception ex)
            {
                // Repack to Http error.
                var httpError = new HttpResponseMessage()
                {
                    StatusCode = (HttpStatusCode)422, // Unprocessable Entity
                    ReasonPhrase = ex.Message
                };

                throw new HttpResponseException(httpError);
            }

            return result;
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
        [Route("ListLeavesByEmployee")]
        public ListLeavesResponse ListLeavesByEmployee(int maximumRows = 10, int startRowIndex = 1,
            string sortExpression = null, string employee = null, LeaveCategories? category = null,
            LeaveStatuses? status = null)
        {
            int totalRowCount = 0;
            var response = new ListLeavesResponse();

            try
            {
                LeaveComponent bc = new LeaveComponent();
                response.Leaves = bc.ListLeavesByEmployee(maximumRows, startRowIndex, sortExpression,
                    employee, category, status, out totalRowCount);
                response.TotalRowCount = totalRowCount;
            }
            catch (Exception ex)
            {
                // Repack to Http error.
                var httpError = new HttpResponseMessage()
                {
                    StatusCode = (HttpStatusCode)422, // Unprocessable Entity
                    ReasonPhrase = ex.Message
                };

                throw new HttpResponseException(httpError);
            }

            return response;
        }

        /// <summary>
        /// Calls the ListLogsByLeave business method of the LeaveComponent.
        /// </summary>
        /// <param name="leaveID"> A leaveID value.</param>
        /// <returns>Returns a List<LeaveStatusLog> object.</returns>
        [HttpGet]
        [Route("ListLogsByLeave/{leaveID}")]
        public List<LeaveStatusLog> ListLogsByLeave(long leaveID)
        {
            List<LeaveStatusLog> result = null;

            try
            {
                LeaveComponent bc = new LeaveComponent();
                result = bc.ListLogsByLeave(leaveID);

            }
            catch (Exception ex)
            {
                // Repack to Http error.
                var httpError = new HttpResponseMessage()
                {
                    StatusCode = (HttpStatusCode)422, // Unprocessable Entity
                    ReasonPhrase = ex.Message
                };

                throw new HttpResponseException(httpError);
            }

            return result;
        }
    }
}
