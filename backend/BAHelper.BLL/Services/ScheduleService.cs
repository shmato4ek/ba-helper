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

        //public async Task<ScheduleDTO> CreateSchedule(NewScheduleDTO newSchedule)
        //{
        //    var scheduleEntity = _mapper.Map<Schedule>(newSchedule);
        //    _context.Schedules.Add(scheduleEntity);
        //    await _context.SaveChangesAsync();
        //    return _mapper.Map<ScheduleDTO>(scheduleEntity);
        //}

        //public async Task<ScheduleDTO> ConfigureNewDay(NewScheduleDayDTO newScheduleDay)
        //{
        //    var scheduleDayEntity = _mapper.Map<ScheduleDay>(newScheduleDay);
        //    var scheduleEntity = await _context.Schedules.FirstOrDefaultAsync(schedule => schedule.Id == newScheduleDay.ScheduleId);
        //    if (scheduleEntity != null)
        //    {
        //        scheduleEntity.Days.Add(scheduleDayEntity);
        //        _context.Schedules.Update(scheduleEntity);
        //        return _mapper.Map<ScheduleDTO>(scheduleEntity);
        //    }
        //    return null;
        //}

        //public async Task<ScheduleDTO> ConfigureSchedule(int projectId, HoursOfWork hoursOfWork)
        //{
        //    var projectEntity = await _context.Projects.FirstOrDefaultAsync(project => project.Id == projectId);
        //    if (projectEntity != null)
        //    {
        //        projectEntity.Schedule.Days = SetScheduleDays(hoursOfWork, projectEntity.Schedule.Id);
        //        return _mapper.Map<ScheduleDTO>(projectEntity.Schedule);
        //    }
        //    return null;
        //}

        //public async Task<ScheduleDayDTO> AddTaskToSchedule(int taskId, int projectId, DayOfWeek dayOfWeek)
        //{
        //    var projectEntity = await _context.Projects.FirstOrDefaultAsync(project => project.Id == projectId);
        //    if(projectEntity != null)
        //    {
        //        var taskEntity = await _context.Tasks.FirstOrDefaultAsync(task => task.Id == taskId);
        //        if (taskEntity != null)
        //        {
        //            var scheduleDayEntity = projectEntity.Schedule.Days.FirstOrDefault(day => day.DayOfWeek == dayOfWeek);
        //            if (scheduleDayEntity != null)
        //            {
        //                if (scheduleDayEntity.AwailableHoursOfWork >= taskEntity.TimeForTask)
        //                {
        //                    scheduleDayEntity.Tasks.Add(taskEntity);
        //                    _context.SchedulDays.Update(scheduleDayEntity);
        //                    await _context.SaveChangesAsync();
        //                    return _mapper.Map<ScheduleDayDTO>(scheduleDayEntity);
        //                }
        //                return null;
        //            }
        //            return null;
        //        }
        //        return null;
        //    }
        //    return null;
        //}

        //public async Task<ScheduleDayDTO> GetScheduleDay(int projectId, DayOfWeek dayOfWeek)
        //{
        //    var projectEntity = await _context.Projects.FirstOrDefaultAsync(project => project.Id == projectId);
        //    if (projectEntity != null)
        //    {
        //        var scheduleDayEntity = projectEntity.Schedule.Days.FirstOrDefault(day => day.DayOfWeek == dayOfWeek);
        //        if (scheduleDayEntity != null)
        //        {
        //            return _mapper.Map<ScheduleDayDTO>(scheduleDayEntity);
        //        }
        //        return null;
        //    }
        //    return null;
        //}

        //public async Task<List<ScheduleDayDTO>> GetSchedule(int projectId)
        //{
        //    var projectEntity = await _context.Projects.FirstOrDefaultAsync(project => project.Id == projectId);
        //    if (projectEntity != null)
        //    {
        //        var schedule = projectEntity.Schedule.Days.ToList();
        //        return _mapper.Map<List<ScheduleDayDTO>>(schedule);
        //    }
        //    return null;
        //}

        //private List<ScheduleDay> SetScheduleDays(HoursOfWork hoursOfWork, int scheduleId)
        //{
        //    List<ScheduleDay> scheduleDays = new List<ScheduleDay>();

        //    NewScheduleDayDTO monday = new NewScheduleDayDTO();
        //    monday.HoursOfWork = hoursOfWork.MondayHours;
        //    monday.ScheduleId = scheduleId;
        //    monday.DayOfWeek = DayOfWeek.Monday;
        //    monday.AvailableHoursOfWork = hoursOfWork.MondayHours;
        //    scheduleDays.Add(_mapper.Map<ScheduleDay>(monday));

        //    NewScheduleDayDTO tuesday = new NewScheduleDayDTO();
        //    tuesday.HoursOfWork = hoursOfWork.TuesdayHours;
        //    tuesday.ScheduleId = scheduleId;
        //    tuesday.DayOfWeek = DayOfWeek.Tuesday;
        //    tuesday.AvailableHoursOfWork = hoursOfWork.TuesdayHours;
        //    scheduleDays.Add(_mapper.Map<ScheduleDay>(tuesday));

        //    NewScheduleDayDTO wednesday = new NewScheduleDayDTO();
        //    wednesday.HoursOfWork = hoursOfWork.WednesdayHours;
        //    wednesday.ScheduleId = scheduleId;
        //    wednesday.DayOfWeek = DayOfWeek.Wednesday;
        //    wednesday.AvailableHoursOfWork = hoursOfWork.WednesdayHours;
        //    scheduleDays.Add(_mapper.Map<ScheduleDay>(wednesday));

        //    NewScheduleDayDTO thursday = new NewScheduleDayDTO();
        //    thursday.HoursOfWork = hoursOfWork.ThursdayHours;
        //    thursday.ScheduleId = scheduleId;
        //    thursday.DayOfWeek = DayOfWeek.Thursday;
        //    thursday.AvailableHoursOfWork = hoursOfWork.ThursdayHours;
        //    scheduleDays.Add(_mapper.Map<ScheduleDay>(thursday));

        //    NewScheduleDayDTO friday = new NewScheduleDayDTO();
        //    friday.HoursOfWork = hoursOfWork.FridayHours;
        //    friday.ScheduleId = scheduleId;
        //    friday.DayOfWeek = DayOfWeek.Friday;
        //    friday.AvailableHoursOfWork = hoursOfWork.FridayHours;
        //    scheduleDays.Add(_mapper.Map<ScheduleDay>(friday));

        //    NewScheduleDayDTO saturday = new NewScheduleDayDTO();
        //    saturday.HoursOfWork = hoursOfWork.SaturdayHours;
        //    saturday.ScheduleId = scheduleId;
        //    saturday.DayOfWeek = DayOfWeek.Saturday;
        //    saturday.AvailableHoursOfWork = hoursOfWork.SaturdayHours;
        //    scheduleDays.Add(_mapper.Map<ScheduleDay>(saturday));

        //    NewScheduleDayDTO sunday = new NewScheduleDayDTO();
        //    sunday.HoursOfWork = hoursOfWork.SundayHours;
        //    sunday.ScheduleId = scheduleId;
        //    sunday.DayOfWeek = DayOfWeek.Sunday;
        //    sunday.AvailableHoursOfWork = hoursOfWork.SundayHours;
        //    scheduleDays.Add(_mapper.Map<ScheduleDay>(sunday));

        //    return scheduleDays;
        //}
    }
}
