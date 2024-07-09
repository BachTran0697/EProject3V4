using eProject3.Interfaces;
using eProject3.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eProject3.Repositories
{
    public class ReservedRepo : IReservedRepo
    {
        private readonly DatabaseContext db;
        public ReservedRepo(DatabaseContext db)
        {
            this.db = db;
        }

        public async Task<Reservation> GetByPRNAsync(string prn)
        {
            return await db.Reservations.FirstOrDefaultAsync(r => r.Ticket_code == prn);
        }

        public async Task UpdateAsync(Reservation reservation)
        {
            db.Reservations.Update(reservation);
            await db.SaveChangesAsync();
        }

        public async Task<Reservation> CreateReservation(Reservation reservation)
        {
            // Check seat availability
            var seatAvailable = await db.Seats
                .Include(s => s.SeatDetails)
                .FirstOrDefaultAsync(s => s.Id == reservation.Seat_id &&
                                          s.CoachId == reservation.Coach_id &&
                                          s.SeatDetails.All(sd => sd.Status != "Reserved"));

            if (seatAvailable == null)
            {
                throw new Exception("Seat is not available.");
            }

            // Generate ticket code
            reservation.Ticket_code = GenerateTicketCode();

            // Create new reservation
            var newReservation = new Reservation
            {
                Name = reservation.Name,
                Email = reservation.Email,
                Phone = reservation.Phone,
                Ticket_code = reservation.Ticket_code,
                Station_begin_id = reservation.Station_begin_id,
                Station_end_id = reservation.Station_end_id,
                Time_begin = reservation.Time_begin,
                Time_end = reservation.Time_end,
                Train_id = reservation.Train_id,
                Coach_id = reservation.Coach_id,
                Seat_id = reservation.Seat_id,
                Price = reservation.Price,
                IsCancelled = false
            };

            db.Reservations.Add(newReservation);
            await db.SaveChangesAsync();

            // Mark seat as reserved
            var newSeatDetail = new SeatDetail
            {
                Status = "Reserved",
                Station_code_begin = reservation.Station_begin_id,
                Station_code_end = reservation.Station_end_id,
                SeatId = reservation.Seat_id
            };

            db.SeatDetails.Add(newSeatDetail);
            await db.SaveChangesAsync();

            return newReservation;
        }

        private string GenerateTicketCode()
        {
            string dateTimePart = DateTime.Now.ToString("yyyyMMddHHmmss");
            string randomPart = Guid.NewGuid().ToString().Substring(0, 4).ToUpper();
            return $"{dateTimePart}{randomPart}";
        }

        public async Task<Reservation> FinishReservation(int id)
        {
            var reservation = await GetReservationById(id);
            if (reservation == null)
            {
                throw new ArgumentException("No reservation found with the provided ID.");
            }

            if (reservation.Time_end <= DateTime.Now)
            {
                var seatDetail = await db.SeatDetails
                    .FirstOrDefaultAsync(sd => sd.SeatId == reservation.Seat_id && sd.Status == "Reserved");

                if (seatDetail != null)
                {
                    seatDetail.Status = "Finished";
                    db.SeatDetails.Update(seatDetail);
                    await db.SaveChangesAsync();
                }
            }

            return reservation;
        }

        public async Task<Reservation> GetReservationById(int id)
        {
            return await db.Reservations.SingleOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Reservation>> GetReservations()
        {
            return await db.Reservations.ToListAsync();
        }

        public async Task<Reservation> UpdateReservation(Reservation reservation)
        {
            var existingReservation = await GetReservationById(reservation.Id);
            if (existingReservation == null)
            {
                throw new ArgumentException("No reservation found with the provided ID.");
            }

            existingReservation.Name = reservation.Name ?? existingReservation.Name;
            existingReservation.Email = reservation.Email ?? existingReservation.Email;
            existingReservation.Phone = reservation.Phone ?? existingReservation.Phone;
            existingReservation.Station_begin_id = reservation.Station_begin_id;
            existingReservation.Station_end_id = reservation.Station_end_id;
            existingReservation.Time_begin = reservation.Time_begin;
            existingReservation.Time_end = reservation.Time_end;
            existingReservation.Train_id = reservation.Train_id;
            existingReservation.Coach_id = reservation.Coach_id;
            existingReservation.Seat_id = reservation.Seat_id;
            existingReservation.Price = reservation.Price;
            existingReservation.IsCancelled = reservation.IsCancelled;

            db.Reservations.Update(existingReservation);
            await db.SaveChangesAsync();

            return existingReservation;
        }
    }
}
