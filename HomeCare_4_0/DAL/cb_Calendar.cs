using HomeCare_4_0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HomeCare_4_0.Controllers;

namespace HomeCare_4_0.DAL
{
    public class cb_Calendar
    {
        private HomeCareDBEntities context;
        public cb_Calendar()
        {
            this.context = new HomeCareDBEntities();
        }

        public List<HC_SP_Get_Canlendar_Result> GetCandarList(int ProjectID,int UserID)
        {
            return context.HC_SP_Get_Canlendar( UserID, ProjectID).ToList();
        }

        public int? InsertCalendar(CalendarEvent requestCalendarEvent)
        {
            DateTime Start = DateTime.Parse(requestCalendarEvent.start);
            DateTime End = DateTime.Parse(requestCalendarEvent.end);
            return context.HC_SP_Insert_TD_Calendar(requestCalendarEvent.projectID,Start,End,requestCalendarEvent.title, requestCalendarEvent.CreatedBy,requestCalendarEvent.ReferenceID, requestCalendarEvent.ReferenceType).FirstOrDefault();
        }
        public int? UpdateCalendar(CalendarEvent requestCalendarEvent)
        {
            DateTime Start = DateTime.Parse(requestCalendarEvent.start);
            DateTime End = DateTime.Parse(requestCalendarEvent.end);
            return context.HC_SP_Update_TD_Calendar(requestCalendarEvent.calendarID, Start, End, requestCalendarEvent.title, requestCalendarEvent.CreatedBy,false);
        }

        public void DeleteCalendar(CalendarEvent requestCalendarEvent)
        {
            context.HC_SP_Delete_TD_Calendar(requestCalendarEvent.calendarID, requestCalendarEvent.CreatedBy);
        }
        public string[] GetHCEmail(long ProjectID,int UserID)
        {
            
            List<HC_SP_EmailHC_Result> EmailHCList = context.HC_SP_Get_EmailHC(ProjectID, UserID).ToList();
            string[] HCEmail = new string[EmailHCList.Count];
            int i = 0;
            foreach (var item in EmailHCList)
            {                
                HCEmail[i] = item.Email;
                i++;
            }
            return HCEmail;
        }

    }
}