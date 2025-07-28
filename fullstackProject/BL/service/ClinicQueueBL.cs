using BL.API;
using BL.Exceptions;
using BL.Models;
using DAL.Models;
namespace BL.service
{
    public class ClinicQueueBL : IClinicQueueBL
    {
        private readonly IManagerDAL _managerDal;
        public ClinicQueueBL(IManagerDAL managerDAL)
        {
            _managerDal = managerDAL;
        }
        public async Task MakeAnAppointment(string idDoctor, string idClient, DateOnly d, int hour)
        {
            DateTime date = d.ToDateTime(new TimeOnly(hour, 0));
            if (await AvailableQueueManager.Instance.IsHolidayAsync(date))
                throw new DayOfQueueIsNotPermission(date);
             Client? client = await _managerDal._clientDAL.GetClientById(idClient) ?? throw new ClientNotExistException(idClient);
            int doctorId = await _managerDal._doctorDAL.GetDoctorIdByIdNumber(idDoctor);
            if (doctorId == 0)
                throw new DoctorNotExistException(idDoctor);
            bool success = await _managerDal._availableQueueDAL.MakeAnAppointment(doctorId, client, date);
            if (!success)
                throw new AvailableQueueNotFoundException(idDoctor, date);
        }
        public async Task DeleteAnApointment(string idDoctor, string idClient, DateOnly d)
        {
            DateTime date = d.ToDateTime(TimeOnly.MinValue);
            bool queueDeleted= await _managerDal._clinicQueueDAL.DeleteAnApointment(idDoctor, idClient, date);
            if (!queueDeleted)
                throw new QueueDoesNotExistException(idDoctor, idClient, date);
        }
        public async Task GenerateFutureAvailableQueues()
        {
            DateTime start = DateTime.Today;
            DateTime end = start.AddDays(180);
            if (_managerDal == null || _managerDal._doctorDAL == null)
                throw new Exception("_managerDal or _doctorDAL is null");
            var doctors = await _managerDal._doctorDAL.GetDoctorsWithDays() ?? throw new Exception("doctors is null");
            foreach (var doctor in doctors)
            {
                if (doctor.DayDoctors == null) continue; 
                foreach (var dayDoctor in doctor.DayDoctors)
                {
                    if (dayDoctor.Day == null) continue; 
                    var day = dayDoctor.Day;
                    for (DateTime date = start; date <= end; date = date.AddDays(1))
                    {
                        if ((int)date.DayOfWeek == day.DayNum)
                        {
                            for (int h = day.StartHour; h < day.EndHour; h++)
                            {
                                var AppointmentDate = new DateTime(date.Year, date.Month, date.Day, h, 0, 0);
                                if (_managerDal._availableQueueDAL == null)
                                    throw new Exception("_availableQueueDAL is null");
                                var exists = await _managerDal._availableQueueDAL
                                    .GetDoctorAvailableQueueForASpecificHour(doctor.DoctorId, AppointmentDate);
                                if (exists == null)
                                {
                                    var Appointment = new AvailableQueue(AppointmentDate, doctor);
                                    await _managerDal._availableQueueDAL.AddAvailableQueue(Appointment);
                                }
                            }
                        }
                    }
                }
            }
        }
        public async Task<List<M_ClinicQueue>> GetClientQueues(string clientId)
        {
            var queues = await _managerDal._clinicQueueDAL.GetClientsQueues(clientId);

            var result = queues.Select(q => new M_ClinicQueue
            {
                QueueId = q.QueueId,
                AppointmentDate = q.AppointmentDate,
                ClientId = q.Client.ClientId,
                ClientFirstName = q.Client.FirstName,
                ClientLastName = q.Client.LastName,
                DoctorId = q.Doctor.DoctorId,
                DoctorFirstName = q.Doctor.FirstName,
                DoctorLastName = q.Doctor.LastName
            }).ToList();

            return result;
        }

        public async Task<List<int>> ClientsNames(int doctorID)
        {
            return await _managerDal._clinicQueueDAL.ClientsNames(doctorID);
        }


    }
}
