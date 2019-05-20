using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClinicSystem.Infrastructure.Interfaces;
using ClinicSystem.Infrastructure.Model;
using ClinicSystem.WebApplication.Interfaces;

namespace ClinicSystem.WebApplication.Services
{
    public class VisitValidationService : IVisitValidationService
    {
        private readonly IClinicSystemDbContext _db;

        public VisitValidationService(IClinicSystemDbContext db)
        {
            _db = db;
        }

        public string IsPatientVisitValid(PATIENT_VISIT patientVisit)
        {
            var validationResult = "";

            if (patientVisit.DATE_FROM <= DateTime.Now)
            {
                validationResult = "Nie można zarezerwować wizyty na datę i godzinę z przeszłości.";
                return validationResult;
            }

            var existingPatientVisit = _db.PATIENT_VISIT.FirstOrDefault(e =>
                e.CLINIC_ID == patientVisit.CLINIC_ID && e.UNIT_ID == patientVisit.UNIT_ID &&
                e.DATE_FROM >= patientVisit.DATE_FROM && e.DATE_TO <= patientVisit.DATE_TO);

            if (existingPatientVisit != null)
            {
                var allExistingVisitDates = _db.PATIENT_VISIT.Where(e =>
                    e.CLINIC_ID == patientVisit.CLINIC_ID &&
                    e.UNIT_ID == patientVisit.UNIT_ID &&
                    e.DATE_FROM.Day == patientVisit.DATE_FROM.Day &&
                    e.DATE_FROM.Month == patientVisit.DATE_FROM.Month &&
                    e.DATE_FROM.Year == patientVisit.DATE_FROM.Year)
                    .Select(e => e.DATE_FROM).ToList();

                var startFrom = new DateTime(patientVisit.DATE_FROM.Year, patientVisit.DATE_FROM.Month, patientVisit.DATE_FROM.Day, 9, 0, 0);
                var validHours = "";
                validationResult = $"Wskazana godzina jest już zarezerwowana. Pozostałe dostępne godziny dnia { startFrom.ToString("dd.MM.yyyy") }: ";

                while (startFrom < new DateTime(patientVisit.DATE_FROM.Year, patientVisit.DATE_FROM.Month,
                           patientVisit.DATE_FROM.Day, 17, 0, 0))
                {
                    if (!allExistingVisitDates.Contains(startFrom))
                    {
                        validHours += startFrom.ToString("HH:mm") + ", ";
                    }

                    startFrom = startFrom.AddMinutes(30);
                }

                if (string.IsNullOrEmpty(validHours))
                {
                    validationResult += "Brak.";
                }
                else
                {
                    validationResult += validHours.Remove(validHours.Length - 2);
                }
            }

            return validationResult;
        }
    }
}