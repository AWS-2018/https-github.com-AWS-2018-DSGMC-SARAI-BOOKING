using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BusinessObjects.Common;
using DataLayer.Common;

using FrameWork.Core;
using Facade.Common;
using System.ComponentModel;
using DataLayer.Common;

namespace Facade.Common
{
    public class HolidayMasterFacade
    {
        HolidayMasterDao daoObject = new HolidayMasterDao();
        CommonFacade common = new CommonFacade();

        /// <summary>
        /// Method to Save/Update the Information
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Update)]
        public int SaveData(Holiday obj)
        {
            int Id = 0;

            try
            {
                //Checking the Validations
                string validationResult = obj.CheckValidation();

                if (validationResult.Length > 0)
                    throw new ApplicationException(validationResult);

                
                //Starting Transaction
                using (TransactionDecorator transaction = new TransactionDecorator())
                {
                    //Saving Data by calling Dao Method
                    Id = daoObject.SaveData(obj);

                    //If Id is returned as Zero from Data Layer, then throwing Exception and rolling back the Transaction
                    if (Id == 0 && obj.Id == 0)
                        throw new ApplicationException("Error Saving Data.", null);

                    //If Id is returned as Zero from Data Layer, then throwing Exception and rolling back the Transaction
                    if (Id == 0 && obj.Id != 0)
                        throw new ApplicationException("Record has been modified by some other user. Please reload the record and then modify.", null);

                    //If no Error, then Commiting Transaction
                    transaction.Complete();
                }
            }
            catch (ApplicationException ex)
            {
                Id = 0;
                throw new ApplicationException(ex.Message, null);
            }

            return Id;
        }

        /// <summary>
        /// Method to return the List of Data
        /// </summary>
        /// <returns>List of Data</returns>
        public List<Holiday> GetList()
        {
            List<Holiday> lstData = new List<Holiday>();

            try
            {
                //calling Dao Method to get the List of Data
                lstData = daoObject.GetList("");
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message, null);
            }

            return lstData;
        }

        /// <summary>
        /// Method to return the List of Data
        /// </summary>
        /// <returns>List of Data</returns>
        public List<Holiday> GetListForListPage(int page)
        {
            List<Holiday> lstData = new List<Holiday>();

            try
            {
                //calling Dao Method to get the List of Data
                lstData = daoObject.GetList(page, Localizer.CurrentUser.PageSize);
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message, null);
            }

            return lstData;
        }

        /// <summary>
        /// Method to return the List of Data for Entry
        /// </summary>
        /// <returns>List of Data</returns>
        public List<Holiday> GetListForEntry(int selectedId)
        {
            List<Holiday> lstData = GetList();

            try
            {
                //HolidayMaster data = new HolidayMaster();
                //data.Id = -1;
                //data.Name = "[Select One]";
                //lstData.Insert(0, data);
                //data = null;
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message, null);
            }

            return lstData.Where(m => m.IsActive == true || (m.IsActive == false && m.Id == selectedId) || m.Id == 0).ToList();
        }



        /// <summary>
        /// Method to return the List of Data based on the passed Name
        /// </summary>
        /// <returns>List of Data</returns>
        public List<Holiday> GetList(string name)
        {
            List<Holiday> lstData = new List<Holiday>();

            try
            {
                //calling Dao Method to get the List of Data
                lstData = daoObject.GetList(name.Trim());
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message, null);
            }

            return lstData;
        }

        /// <summary>
        /// Method used to Get the Details based on the ID Passed
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Holiday GetDetailById(int id)
        {
            Holiday obj = new Holiday();

            try
            {
                if (id <= 0)
                    throw new Exception("Please pass a valid Id.");

                //calling Dao Method to get the Details
                obj = daoObject.GetSingleRecordDetail(id, null, 0);

                //If Id value is Zero, then raising exception
                if (obj.Id == 0)
                    throw new Exception("Error reading Details.");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, null);
            }

            return obj;
        }

        /// <summary>
        /// Method used to Get the Details based on the date and Institute Id Passed
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Holiday GetDetailByDateAndInstituteId(DateTime? date, int instituteId)
        {
            Holiday obj = new Holiday();

            try
            {
                if (instituteId <= 0)
                    throw new Exception("<li>Please pass a valid Institute Id.</li>");

                //calling Dao Method to get the Details
                obj = daoObject.GetSingleRecordDetail(0, date, instituteId);

                if (obj != null)
                    //If Id value is Zero, then raising exception
                    if (obj.Id == 0)
                        throw new Exception("<li>Error reading Details.</li>");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, null);
            }

            return obj;
        }

        /// <summary>
        /// Method to get the List of Calendar Events
        /// </summary>
        /// <returns></returns>
        public List<CalendarEntries> GetInstituteCalendar(int Month, int Year, string UserType)
        {
            List<CalendarEntries> lstData = new List<CalendarEntries>();

            UserType = UserType == null ? "" : UserType.Trim().ToUpper();

            if (Month <= 0)
                Month = DateTime.Now.Month;

            if (Year <= 0)
                Year = DateTime.Now.Year;

            try
            {
                lstData = daoObject.GetInstituteCalendar(Month, Year, UserType);
            }
            catch(Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }

            return lstData;
        }

        /// <summary>
        /// Method to get the List of Holidays for Mobile Application
        /// </summary>
        /// <returns></returns>
        public List<Holiday> GetHolidayListForMobileApp()
        {
            List<Holiday> lstData = new List<Holiday>();

            try
            {
                lstData = daoObject.GetHolidayListForMobileApp();
            }
            catch(Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }

            return lstData;
        }
    }
}

