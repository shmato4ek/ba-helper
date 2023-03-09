using AutoMapper;
using BAHelper.BLL.Services.Abstract;
using BAHelper.Common;
using BAHelper.Common.DTOs.Schedule;
using BAHelper.Common.DTOs.ScheduleDay;
using BAHelper.DAL.Context;
using BAHelper.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.BLL.Services
{
    public class ScheduleService : BaseService
    {
        public ScheduleService(BAHelperDbContext context, IMapper mapper)
            : base(context, mapper) { }

        public async Task<ScheduleDTO> CreateSchedule(NewScheduleDTO newSchedule)
        {
            var scheduleEntity = _mapper.Map<Schedule>(newSchedule);
            _context.Schedules.Add(scheduleEntity);
            await _context.SaveChangesAsync();
            return _mapper.Map<ScheduleDTO>(scheduleEntity);
        }

        public async Task<ScheduleDTO> ConfigureNewDay(NewScheduleDayDTO newScheduleDay)
        {
            var scheduleDayEntity = _mapper.Map<ScheduleDay>(newScheduleDay);
            var scheduleEntity = await _context.Schedules.FirstOrDefaultAsync(schedule => schedule.Id == newScheduleDay.ScheduleId);
            if (scheduleEntity != null)
            {
                scheduleEntity.Days.Add(scheduleDayEntity);
                _context.Schedules.Update(scheduleEntity);
                return _mapper.Map<ScheduleDTO>(scheduleEntity);
            }
            return null;
        }

        public async Task<ScheduleDTO> ConfigureSchedule(int projectId, HoursOfWork hoursOfWork)
        {
            var projectEntity = await _context.Projects.FirstOrDefaultAsync(project => project.Id == projectId);
            if (projectEntity != null)
            {
                projectEntity.Schedule.Days = SetScheduleDays(hoursOfWork, projectEntity.Schedule.Id);

            }
        }

        private List<ScheduleDay> SetScheduleDays(HoursOfWork hoursOfWork, int scheduleId)
        {
            List<ScheduleDay> scheduleDays = new List<ScheduleDay>();

            NewScheduleDayDTO monday = new NewScheduleDayDTO();
            monday.HoursOfWeeks = hoursOfWork.MondayHours;
            monday.ScheduleId = scheduleId;
            monday.DayOfWeek = DayOfWeek.Monday;
            scheduleDays.Add(_mapper.Map<ScheduleDay>(monday));

            NewScheduleDayDTO tuesday = new NewScheduleDayDTO();
            tuesday.HoursOfWeeks = hoursOfWork.TuesdayHours;
            tuesday.ScheduleId = scheduleId;
            tuesday.DayOfWeek = DayOfWeek.Tuesday;
            scheduleDays.Add(_mapper.Map<ScheduleDay>(tuesday));

            NewScheduleDayDTO wednesday = new NewScheduleDayDTO();
            wednesday.HoursOfWeeks = hoursOfWork.WednesdayHours;
            wednesday.ScheduleId = scheduleId;
            wednesday.DayOfWeek = DayOfWeek.Wednesday;
            scheduleDays.Add(_mapper.Map<ScheduleDay>(wednesday));

            NewScheduleDayDTO thursday = new NewScheduleDayDTO();
            thursday.HoursOfWeeks = hoursOfWork.ThursdayHours;
            thursday.ScheduleId = scheduleId;
            thursday.DayOfWeek = DayOfWeek.Thursday;
            scheduleDays.Add(_mapper.Map<ScheduleDay>(thursday));

            NewScheduleDayDTO friday = new NewScheduleDayDTO();
            friday.HoursOfWeeks = hoursOfWork.FridayHours;
            friday.ScheduleId = scheduleId;
            friday.DayOfWeek = DayOfWeek.Friday;
            scheduleDays.Add(_mapper.Map<ScheduleDay>(friday));

            NewScheduleDayDTO saturday = new NewScheduleDayDTO();
            saturday.HoursOfWeeks = hoursOfWork.SaturdayHours;
            saturday.ScheduleId = scheduleId;
            saturday.DayOfWeek = DayOfWeek.Saturday;
            scheduleDays.Add(_mapper.Map<ScheduleDay>(saturday));

            NewScheduleDayDTO sunday = new NewScheduleDayDTO();
            sunday.HoursOfWeeks = hoursOfWork.SundayHours;
            sunday.ScheduleId = scheduleId;
            sunday.DayOfWeek = DayOfWeek.Sunday;
            scheduleDays.Add(_mapper.Map<ScheduleDay>(sunday));

            return scheduleDays;
        }
    }
}
