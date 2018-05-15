namespace NUnit.Tests
{
    using Car_Service.DAL.Entities;
    using Car_Service.DAL.Interfaces;
    using Car_Service.Model.Interfaces;
    using Car_Service.BLL.Services;
    using FluentAssertions;
    using Moq;
    using System;
    using NUnit.Framework;
    using System.Collections.Generic;
    using Car_Service.BLL.DTO;
    using System.Linq;
    using Car_Service.BLL.Interfaces;

    [TestFixture]
    public class Tests
    {
        [Test]
        public void ConfirmReservation_NonexistentGUIDParameter_FalseResult()
        {
            // arrange
            var confirmReservationManagerMock = new Mock<IConfirmReservation>();
            confirmReservationManagerMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns((ConfirmReservation)null);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(m => m.ConfirmReservationManager).Returns(confirmReservationManagerMock.Object);
            var clockMock = new Mock<IClock>();

            var reservationService = new ReservationService(unitOfWorkMock.Object, clockMock.Object);

            // act
            var actual = reservationService.Confirm(Guid.Parse("11111111-1111-1111-1111-111111111111"));

            // assert
            actual.Should().NotBeNull();
            actual.Succedeed.Should().BeFalse();
            actual.Message.Should().Be("Error guid or time extire");
        }
        [Test]
        public void ConfirmReservation_AlreadyConfirm_FalseResult()
        {
            var confirmReservation = new ConfirmReservation
            {
                Id = 1,
                Guid = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                ExpireDate = new DateTime(2008, 11, 11, 8, 0, 0, DateTimeKind.Unspecified),
                IsConfirm = true
            };
            // arrange
            var confirmReservationManagerMock = new Mock<IConfirmReservation>();
            confirmReservationManagerMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(confirmReservation);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(m => m.ConfirmReservationManager).Returns(confirmReservationManagerMock.Object);
            var clockMock = new Mock<IClock>();
            clockMock.Setup(m => m.CurentUtcDateTime()).Returns(new DateTime(2008, 11, 11, 10, 0, 1).ToUniversalTime());

            var reservationService = new ReservationService(unitOfWorkMock.Object, clockMock.Object);

            // act
            var actual = reservationService.Confirm(Guid.Parse("11111111-1111-1111-1111-111111111111"));

            // assert
            actual.Should().NotBeNull();
            actual.Succedeed.Should().BeFalse();
            actual.Message.Should().Be("Error guid or time extire");
        }
        [Test]
        public void ConfirmReservation_ExpireConfirmDate_FalseResult()
        {
            var confirmReservation = new ConfirmReservation
            {
                Id = 1,
                Guid = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                ExpireDate = new DateTime(2008, 11, 11, 8, 0, 0, DateTimeKind.Unspecified),
                IsConfirm = false
            };
            // arrange
            var confirmReservationManagerMock = new Mock<IConfirmReservation>();
            confirmReservationManagerMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(confirmReservation);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(m => m.ConfirmReservationManager).Returns(confirmReservationManagerMock.Object);
            var clockMock = new Mock<IClock>();
            clockMock.Setup(m => m.CurentUtcDateTime()).Returns(new DateTime(2008, 11, 11, 8, 0, 0, DateTimeKind.Utc).AddMinutes(1));

            var reservationService = new ReservationService(unitOfWorkMock.Object, clockMock.Object);

            // act
            var actual = reservationService.Confirm(Guid.Parse("11111111-1111-1111-1111-111111111111"));

            // assert
            actual.Should().NotBeNull();
            actual.Succedeed.Should().BeFalse();
            actual.Message.Should().Be("Error guid or time extire");
        }
        [Test]
        public void ConfirmReservation_TrueGuid_ConfirmReservation()
        {
            var confirmReservation = new ConfirmReservation
            {
                Id = 1,
                Guid = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                ExpireDate = new DateTime(2008, 11, 11, 8, 0, 0, DateTimeKind.Unspecified),
                IsConfirm = false
            };
            // arrange
            var confirmReservationManagerMock = new Mock<IConfirmReservation>();
            confirmReservationManagerMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(confirmReservation);
            confirmReservationManagerMock.Setup(m => m.Update(It.IsAny<ConfirmReservation>())).Verifiable();

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(m => m.ConfirmReservationManager).Returns(confirmReservationManagerMock.Object);
            var clockMock = new Mock<IClock>();
            clockMock.Setup(m => m.CurentUtcDateTime()).Returns(new DateTime(2008, 11, 11, 8, 0, 0, DateTimeKind.Utc));

            var reservationService = new ReservationService(unitOfWorkMock.Object, clockMock.Object);

            // act
            var actual = reservationService.Confirm(Guid.Parse("11111111-1111-1111-1111-111111111111"));

            // assert
            actual.Should().NotBeNull();
            confirmReservationManagerMock.VerifyAll();
            actual.Succedeed.Should().BeTrue();
        }
        [Test]
        public void GetWorker_ListData_ReturnListWorkerDTO()
        {
            // arrange
            var workers = new List<Worker>();
            workers.Add(new Worker { FirstName = "Ivan", SurName = "Ivanov", Id = 1, Email = "ivanow@email.com", Telephone = "+375251234567" });

            var workerManagerMock = new Mock<IWorkerManager>();
            workerManagerMock.Setup(m => m.Get()).Returns(workers);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(m => m.WorkerManager).Returns(workerManagerMock.Object);
            var clockMock = new Mock<IClock>();

            var workerService = new WorkerService(unitOfWorkMock.Object, clockMock.Object);

            // act
            var actual = workerService.GetWorker();

            // assert
            actual.ShouldBeEquivalentTo(workers.Select(s => new WorkerDTO { Id = s.Id, Name = s.FirstName, SurName = s.SurName, Telephone = s.Telephone, Email = s.Email }));
        }
        [Test]
        public void ReservationTimes_DataList_ReturnListTimesDTO()
        {
            // arrange
            var worker = new Worker { FirstName = "Ivan", SurName = "Ivanov", Id = 1, Email = "ivanow@email.com", Telephone = "+375251234567" };
            var worker2 = new Worker { FirstName = "Sergeev", SurName = "Sergey", Id = 2, Email = "sergeew@email.com", Telephone = "+375251234568" };

            var confirm = new ConfirmReservation { IsConfirm = true };

            var workers = new List<Worker>();
            workers.AddRange(new Worker[] {
                worker
            });
            var worker1reservation1 = new Reservation
            {
                Worker = worker,
                DateStart = new DateTime(2008, 09, 2, 22, 0, 0, DateTimeKind.Unspecified),
                DateEnd = new DateTime(2008, 09, 3, 2, 0, 0, DateTimeKind.Unspecified),
                ConfirmReservation = confirm
            };
            var reservation = new List<Reservation>();
            reservation.AddRange(new Reservation[] {
                worker1reservation1
            });
            var workerManagerMock = new Mock<IWorkerManager>();
            workerManagerMock.Setup(m => m.Get()).Returns(workers);

            var reservationManagerMock = new Mock<IReservationManager>();
            reservationManagerMock.Setup(m => m.Get()).Returns(reservation);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(m => m.WorkerManager).Returns(workerManagerMock.Object);
            unitOfWorkMock.Setup(m => m.ReservationManager).Returns(reservationManagerMock.Object);

            var clockMock = new Mock<IClock>();
            clockMock.Setup(m => m.CurentUtcDateTime()).Returns(new DateTime(2008, 09, 3, 12, 0, 0).ToUniversalTime());

            var workerService = new WorkerService(unitOfWorkMock.Object, clockMock.Object);

            // act
            var actual = workerService.ReservationTimes(worker.Id);

            // assert
            var expectedTimeResult = reservation.Select(s => {
                return new TimeDTO
                {
                    StartTime = DateTime.SpecifyKind(s.DateStart, DateTimeKind.Utc),
                    EndTime = DateTime.SpecifyKind(s.DateEnd, DateTimeKind.Utc)
                };
            }).ToList<TimeDTO>();
            actual.ShouldBeEquivalentTo(new TimesDTO
            {
                WorkerId = worker.Id,
                Times = expectedTimeResult
            });
        }
        [Test]
        public void ReservationTimes_ErrorIdFromParams_NullResult()
        {
            // arrange
            var worker = new Worker {  Id = 1 };

            var workers = new List<Worker>();
            workers.AddRange(new Worker[] {
                worker
            });

            var workerManagerMock = new Mock<IWorkerManager>();
            workerManagerMock.Setup(m => m.Get()).Returns(workers);


            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(m => m.WorkerManager).Returns(workerManagerMock.Object);
            var clockMock = new Mock<IClock>();

            var workerService = new WorkerService(unitOfWorkMock.Object, clockMock.Object);

            // act
            var actual = workerService.ReservationTimes(-1);

            // assert
            actual.Should().BeNull();
        }
        [Test]
        public void ReservationTimes_DateWithUnspecifiedKind_ReturnDateWithUTCKind()
        {
            // arrange
            var worker = new Worker { FirstName = "Ivan", SurName = "Ivanov", Id = 1, Email = "ivanow@email.com", Telephone = "+375251234567" };


            var confirm = new ConfirmReservation { IsConfirm = true };

            var workers = new List<Worker>();
            workers.AddRange(new Worker[] {
                worker
            });
            var worker1reservation1 = new Reservation
            {
                Worker = worker,
                DateStart = new DateTime(2008, 09, 2, 22, 0, 0, DateTimeKind.Unspecified),
                DateEnd = new DateTime(2008, 09, 3, 2, 0, 0, DateTimeKind.Unspecified),
                ConfirmReservation = confirm
            };
            var reservation = new List<Reservation>();
            reservation.AddRange(new Reservation[] {
                worker1reservation1
            });
            var workerManagerMock = new Mock<IWorkerManager>();
            workerManagerMock.Setup(m => m.Get()).Returns(workers);

            var reservationManagerMock = new Mock<IReservationManager>();
            reservationManagerMock.Setup(m => m.Get()).Returns(reservation);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(m => m.WorkerManager).Returns(workerManagerMock.Object);
            unitOfWorkMock.Setup(m => m.ReservationManager).Returns(reservationManagerMock.Object);

            var clockMock = new Mock<IClock>();
            clockMock.Setup(m => m.CurentUtcDateTime()).Returns(new DateTime(2008, 09, 3, 12, 0, 0).ToUniversalTime());

            var workerService = new WorkerService(unitOfWorkMock.Object, clockMock.Object);

            // act
            var actual = workerService.ReservationTimes(worker.Id);

            // assert
            actual.Should().NotBeNull();
            actual.Times.First().EndTime.Kind.Should().Be(DateTimeKind.Utc);
            actual.Times.First().StartTime.Kind.Should().Be(DateTimeKind.Utc);
        }
        [Test]
        public void ReservationTimes_DateBeforeAndAfterCurentDate_ReturnDateOnlyBeforeCurentDate()
        {
            // arrange
            var worker = new Worker { FirstName = "Ivan", SurName = "Ivanov", Id = 1, Email = "ivanow@email.com", Telephone = "+375251234567" };


            var confirm = new ConfirmReservation { IsConfirm = true };

            var workers = new List<Worker>();
            workers.AddRange(new Worker[] {
                worker
            });
            var worker1reservation1 = new Reservation
            {
                Worker = worker,
                DateStart = new DateTime(2008, 09, 3, 8, 0, 0, DateTimeKind.Unspecified),
                DateEnd = new DateTime(2008, 09, 3, 9, 0, 0, DateTimeKind.Unspecified),
                ConfirmReservation = confirm
            };
            var worker1reservation2 = new Reservation
            {
                Worker = worker,
                DateStart = new DateTime(2008, 09, 4, 8, 0, 0, DateTimeKind.Unspecified),
                DateEnd = new DateTime(2008, 09, 4, 9, 0, 0, DateTimeKind.Unspecified),
                ConfirmReservation = confirm
            };
            var worker1reservation3 = new Reservation
            {
                Worker = worker,
                DateStart = new DateTime(2008, 09, 3, 22, 0, 0, DateTimeKind.Unspecified),
                DateEnd = new DateTime(2008, 09, 4, 01, 0, 0, DateTimeKind.Unspecified),
                ConfirmReservation = confirm
            };
            var reservation = new List<Reservation>();
            reservation.AddRange(new Reservation[] {
                worker1reservation1
            });
            var workerManagerMock = new Mock<IWorkerManager>();
            workerManagerMock.Setup(m => m.Get()).Returns(workers);

            var reservationManagerMock = new Mock<IReservationManager>();
            reservationManagerMock.Setup(m => m.Get()).Returns(reservation);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(m => m.WorkerManager).Returns(workerManagerMock.Object);
            unitOfWorkMock.Setup(m => m.ReservationManager).Returns(reservationManagerMock.Object);

            var clockMock = new Mock<IClock>();

            var curentDate = new DateTime(2008, 09, 3, 12, 0, 0).ToUniversalTime();
            clockMock.Setup(m => m.CurentUtcDateTime()).Returns(curentDate);

            var workerService = new WorkerService(unitOfWorkMock.Object, clockMock.Object);

            // act
            var actual = workerService.ReservationTimes(worker.Id);

            // assert
            var expectedTimeResult = reservation.Where(s => DateTime.SpecifyKind(s.DateEnd, DateTimeKind.Utc) >= curentDate).Select(s => {
                return new TimeDTO
                {
                    StartTime = DateTime.SpecifyKind(s.DateStart, DateTimeKind.Utc),
                    EndTime = DateTime.SpecifyKind(s.DateEnd, DateTimeKind.Utc)
                };
            }).ToList<TimeDTO>();
            actual.ShouldBeEquivalentTo(new TimesDTO
            {
                WorkerId = worker.Id,
                Times = expectedTimeResult
            });
        }
        [Test]
        public void ReservationTimes_DataWithConfirmAndNotConfirm_ReturnDataWithOnlyConfirmReservation()
        {
            // arrange
            var worker = new Worker { FirstName = "Ivan", SurName = "Ivanov", Id = 1, Email = "ivanow@email.com", Telephone = "+375251234567" };

            var workers = new List<Worker>();
            workers.AddRange(new Worker[] {
                worker
            });
            var worker1reservation1 = new Reservation
            {
                Worker = worker,
                DateStart = new DateTime(2008, 09, 2, 22, 0, 0, DateTimeKind.Unspecified),
                DateEnd = new DateTime(2008, 09, 3, 2, 0, 0, DateTimeKind.Unspecified),
                ConfirmReservation = new ConfirmReservation { IsConfirm = true }
            };
            var worker1reservation2 = new Reservation
            {
                Worker = worker,
                DateStart = new DateTime(2008, 09, 2, 22, 0, 0, DateTimeKind.Unspecified),
                DateEnd = new DateTime(2008, 09, 3, 2, 0, 0, DateTimeKind.Unspecified),
                ConfirmReservation = new ConfirmReservation { IsConfirm = false }
            };
            var reservation = new List<Reservation>();
            reservation.AddRange(new Reservation[] {
                worker1reservation1,
                worker1reservation2
            });
            var workerManagerMock = new Mock<IWorkerManager>();
            workerManagerMock.Setup(m => m.Get()).Returns(workers);

            var reservationManagerMock = new Mock<IReservationManager>();
            reservationManagerMock.Setup(m => m.Get()).Returns(reservation);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(m => m.WorkerManager).Returns(workerManagerMock.Object);
            unitOfWorkMock.Setup(m => m.ReservationManager).Returns(reservationManagerMock.Object);

            var clockMock = new Mock<IClock>();
            clockMock.Setup(m => m.CurentUtcDateTime()).Returns(new DateTime(2008, 09, 3, 12, 0, 0).ToUniversalTime());

            var workerService = new WorkerService(unitOfWorkMock.Object, clockMock.Object);

            // act
            var actual = workerService.ReservationTimes(worker.Id);

            // assert
            var expectedTimeResult = reservation.Where(s => s.ConfirmReservation.IsConfirm).Select(s => {
                return new TimeDTO
                {
                    StartTime = DateTime.SpecifyKind(s.DateStart, DateTimeKind.Utc),
                    EndTime = DateTime.SpecifyKind(s.DateEnd, DateTimeKind.Utc)
                };
            }).ToList<TimeDTO>();
            actual.ShouldBeEquivalentTo(new TimesDTO
            {
                WorkerId = worker.Id,
                Times = expectedTimeResult
            });
        }
        [Test]
        public void AddWorker_TrueWorkerModel_CreateWorker()
        {
            // arrange
            var workers = new List<Worker>();

            var workerManagerMock = new Mock<IWorkerManager>();
            workerManagerMock.Setup(m => m.Get()).Returns(workers);
            workerManagerMock.Setup(s => s.Create(It.IsAny<Worker>())).Verifiable();

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var clockMock = new Mock<IClock>();
            unitOfWorkMock.Setup(m => m.WorkerManager).Returns(workerManagerMock.Object);

            var workerService = new WorkerService(unitOfWorkMock.Object, clockMock.Object);

            var model = new WorkerDTO
            {
                Name = "Ivan",
                SurName = "Ivanov",
                Telephone = "+375251234567",
                Email = "ivanow@email.com"
            };
            // act
            var actual = workerService.AddWorker(model);
            // assert
            workerManagerMock.VerifyAll();
            actual.Should().NotBeNull();
            actual.Succedeed.Should().BeTrue();
        }
        [Test]
        public void AddWorker_ExistEmail_FalseResult()
        {
            // arrange
            var workers = new List<Worker>();
            workers.Add(new Worker { FirstName = "", SurName = "", Id = 1, Email = "ivanow@email.com", Telephone = "" });

            var workerManagerMock = new Mock<IWorkerManager>();
            workerManagerMock.Setup(m => m.Get()).Returns(workers);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(m => m.WorkerManager).Returns(workerManagerMock.Object);
            var clockMock = new Mock<IClock>();

            var workerService = new WorkerService(unitOfWorkMock.Object, clockMock.Object);

            var model = new WorkerDTO
            {
                Name = "Ivan",
                SurName = "Ivanov",
                Telephone = "+375251234567",
                Email = "ivanow@email.com"
            };

            // act
            var actual = workerService.AddWorker(model);

            // assert
            actual.Should().NotBeNull();
            actual.Succedeed.Should().BeFalse();
        }
        [Test]
        public void WorkerTimes_ListDate_ReturnListTimesDTO()
        {
            // arrange
            var worker = new Worker { FirstName = "Ivan", SurName = "Ivanov", Id = 1, Email = "ivanow@email.com", Telephone = "+375251234567" };
            

            var confirm = new ConfirmReservation { IsConfirm = true };

            var workers = new List<Worker>();
            workers.AddRange(new Worker[] {
                worker
            });
            var worker1time1 = new WorkTime
            {
                Worker = worker,
                DateStart = new DateTime(2008, 09, 3, 6, 0, 0, DateTimeKind.Unspecified),
                DateEnd = new DateTime(2008, 09, 3, 10, 0, 0, DateTimeKind.Unspecified)
            };

            var workTime = new List<WorkTime>();
            workTime.AddRange(new WorkTime[] {
                worker1time1
            });
            var workerManagerMock = new Mock<IWorkerManager>();
            workerManagerMock.Setup(m => m.Get()).Returns(workers);

            var workTimeManagerMock = new Mock<IWorkTimeManager>();
            workTimeManagerMock.Setup(m => m.Get()).Returns(workTime);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(m => m.WorkerManager).Returns(workerManagerMock.Object);
            unitOfWorkMock.Setup(m => m.WorkTimeManager).Returns(workTimeManagerMock.Object);

            var clockMock = new Mock<IClock>();
            clockMock.Setup(m => m.CurentUtcDateTime()).Returns(new DateTime(2008, 09, 3, 12, 0, 0).ToUniversalTime());

            var workerService = new WorkerService(unitOfWorkMock.Object, clockMock.Object);

            // act
            var actual = workerService.WorkerTimes(worker.Id);

            // assert
            var expectedTimeResult = workTime.Select(s => {
                return new TimeDTO
                {
                    StartTime = DateTime.SpecifyKind(s.DateStart, DateTimeKind.Utc),
                    EndTime = DateTime.SpecifyKind(s.DateEnd, DateTimeKind.Utc)
                };
            }).ToList<TimeDTO>();
            actual.ShouldBeEquivalentTo(new TimesDTO
            {
                WorkerId = worker.Id,
                Times = expectedTimeResult
            });
        }
        [Test]
        public void WorkerTimes_ErrorIdParams_ReturnNull()
        {
            // arrange
            var worker = new Worker { FirstName = "Ivan", SurName = "Ivanov", Id = 1, Email = "ivanow@email.com", Telephone = "+375251234567" };


            var confirm = new ConfirmReservation { IsConfirm = true };

            var workers = new List<Worker>();
            workers.AddRange(new Worker[] {
                worker
            });
            var worker1time1 = new WorkTime
            {
                Worker = worker,
                DateStart = new DateTime(2008, 09, 3, 6, 0, 0, DateTimeKind.Unspecified),
                DateEnd = new DateTime(2008, 09, 3, 10, 0, 0, DateTimeKind.Unspecified)
            };
            var worker1time2 = new WorkTime
            {
                Worker = worker,
                DateStart = new DateTime(2008, 09, 4, 10, 0, 0, DateTimeKind.Unspecified),
                DateEnd = new DateTime(2008, 09, 4, 20, 0, 0, DateTimeKind.Unspecified)
            };

            var workTime = new List<WorkTime>();
            workTime.AddRange(new WorkTime[] {
                worker1time1,
                worker1time2
            });
            var workerManagerMock = new Mock<IWorkerManager>();
            workerManagerMock.Setup(m => m.Get()).Returns(workers);

            var workTimeManagerMock = new Mock<IWorkTimeManager>();
            workTimeManagerMock.Setup(m => m.Get()).Returns(workTime);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(m => m.WorkerManager).Returns(workerManagerMock.Object);
            unitOfWorkMock.Setup(m => m.WorkTimeManager).Returns(workTimeManagerMock.Object);

            var clockMock = new Mock<IClock>();
            clockMock.Setup(m => m.CurentUtcDateTime()).Returns(new DateTime(2008, 09, 3, 12, 0, 0).ToUniversalTime());

            var workerService = new WorkerService(unitOfWorkMock.Object, clockMock.Object);

            // act
            var actual = workerService.WorkerTimes(-1);

            // assert

            actual.Should().BeNull();
        }
        [Test]
        public void WorkerTimes_DateWithUnspecifiedKind_ReturnDateWithUTCKind()
        {
            // arrange
            var worker = new Worker { FirstName = "Ivan", SurName = "Ivanov", Id = 1, Email = "ivanow@email.com", Telephone = "+375251234567" };


            var confirm = new ConfirmReservation { IsConfirm = true };

            var workers = new List<Worker>();
            workers.AddRange(new Worker[] {
                worker
            });
            var worker1time1 = new WorkTime
            {
                Worker = worker,
                DateStart = new DateTime(2008, 09, 3, 6, 0, 0, DateTimeKind.Unspecified),
                DateEnd = new DateTime(2008, 09, 3, 10, 0, 0, DateTimeKind.Unspecified)
            };
            var worker1time2 = new WorkTime
            {
                Worker = worker,
                DateStart = new DateTime(2008, 09, 4, 10, 0, 0, DateTimeKind.Unspecified),
                DateEnd = new DateTime(2008, 09, 4, 20, 0, 0, DateTimeKind.Unspecified)
            };

            var workTime = new List<WorkTime>();
            workTime.AddRange(new WorkTime[] {
                worker1time1,
                worker1time2
            });
            var workerManagerMock = new Mock<IWorkerManager>();
            workerManagerMock.Setup(m => m.Get()).Returns(workers);

            var workTimeManagerMock = new Mock<IWorkTimeManager>();
            workTimeManagerMock.Setup(m => m.Get()).Returns(workTime);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(m => m.WorkerManager).Returns(workerManagerMock.Object);
            unitOfWorkMock.Setup(m => m.WorkTimeManager).Returns(workTimeManagerMock.Object);

            var clockMock = new Mock<IClock>();
            clockMock.Setup(m => m.CurentUtcDateTime()).Returns(new DateTime(2008, 09, 3, 12, 0, 0).ToUniversalTime());

            var workerService = new WorkerService(unitOfWorkMock.Object, clockMock.Object);

            // act
            var actual = workerService.WorkerTimes(worker.Id);

            // assert
            actual.Should().NotBeNull();
            actual.Times.First().EndTime.Kind.Should().Be(DateTimeKind.Utc);
            actual.Times.First().StartTime.Kind.Should().Be(DateTimeKind.Utc);
        }
        [Test]
        public void WorkerTimes_DateBeforAndAfterCurentDate_ReturnDatesOnlyAfterCurentDate()
        {
            // arrange
            var worker = new Worker { FirstName = "Ivan", SurName = "Ivanov", Id = 1, Email = "ivanow@email.com", Telephone = "+375251234567" };


            var confirm = new ConfirmReservation { IsConfirm = true };

            var workers = new List<Worker>();
            workers.AddRange(new Worker[] {
                worker
            });
            var worker1time1 = new WorkTime
            {
                Worker = worker,
                DateStart = new DateTime(2008, 09, 3, 6, 0, 0, DateTimeKind.Unspecified),
                DateEnd = new DateTime(2008, 09, 3, 10, 0, 0, DateTimeKind.Unspecified)
            };
            var worker1time2 = new WorkTime
            {
                Worker = worker,
                DateStart = new DateTime(2008, 09, 4, 10, 0, 0, DateTimeKind.Unspecified),
                DateEnd = new DateTime(2008, 09, 4, 20, 0, 0, DateTimeKind.Unspecified)
            };

            var workTime = new List<WorkTime>();
            workTime.AddRange(new WorkTime[] {
                worker1time1,
                worker1time2
            });
            var workerManagerMock = new Mock<IWorkerManager>();
            workerManagerMock.Setup(m => m.Get()).Returns(workers);

            var workTimeManagerMock = new Mock<IWorkTimeManager>();
            workTimeManagerMock.Setup(m => m.Get()).Returns(workTime);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(m => m.WorkerManager).Returns(workerManagerMock.Object);
            unitOfWorkMock.Setup(m => m.WorkTimeManager).Returns(workTimeManagerMock.Object);

            var curentDate = new DateTime(2008, 09, 4, 12, 0, 0).ToUniversalTime();
            var clockMock = new Mock<IClock>();
            clockMock.Setup(m => m.CurentUtcDateTime()).Returns(curentDate);

            var workerService = new WorkerService(unitOfWorkMock.Object, clockMock.Object);

            // act
            var actual = workerService.WorkerTimes(worker.Id);

            // assert
            var expectedTimeResult = workTime.Where(s => DateTime.SpecifyKind(s.DateEnd, DateTimeKind.Utc) >= curentDate).Select(s => {
                return new TimeDTO
                {
                    StartTime = DateTime.SpecifyKind(s.DateStart, DateTimeKind.Utc),
                    EndTime = DateTime.SpecifyKind(s.DateEnd, DateTimeKind.Utc)
                };
            }).ToList<TimeDTO>();
            actual.ShouldBeEquivalentTo(new TimesDTO
            {
                WorkerId = worker.Id,
                Times = expectedTimeResult
            });
        }
        [Test]
        public void AddWorkTime_ErrorIdForParameter_FalseReturn()
        {
            // arrange
            var worker = new Worker { FirstName = "Ivan", SurName = "Ivanov", Id = 1, Email = "ivanow@email.com", Telephone = "+375251234567" };
            var workers = new List<Worker>();
            workers.AddRange(new Worker[] {
                worker
            });
            var workerManagerMock = new Mock<IWorkerManager>();
            workerManagerMock.Setup(m => m.Get()).Returns(workers);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(m => m.WorkerManager).Returns(workerManagerMock.Object);
           
            var curentDate = new DateTime(2008, 09, 4, 12, 0, 0, DateTimeKind.Utc);
            var clockMock = new Mock<IClock>();
            clockMock.Setup(m => m.CurentUtcDateTime()).Returns(curentDate);

            var workerService = new WorkerService(unitOfWorkMock.Object, clockMock.Object);
            var model = new WorkTimeDTO
            {
                UserId = -1
            };
            // act
            var actual = workerService.AddWorkTime(model);

            // assert
            actual.Should().NotBeNull();
            actual.Succedeed.Should().BeFalse();
        }
        [Test]
        public void AddWorkTime_StartTimeLessCurentTime_FalseReturn()
        {
            // arrange
            var worker = new Worker { FirstName = "Ivan", SurName = "Ivanov", Id = 1, Email = "ivanow@email.com", Telephone = "+375251234567" };
            var workers = new List<Worker>();
            workers.AddRange(new Worker[] {
                worker
            });
            var workerManagerMock = new Mock<IWorkerManager>();
            workerManagerMock.Setup(m => m.Get()).Returns(workers);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(m => m.WorkerManager).Returns(workerManagerMock.Object);

            var curentDate = new DateTime(2008, 09, 4, 12, 0, 0, DateTimeKind.Utc);
            var clockMock = new Mock<IClock>();
            clockMock.Setup(m => m.CurentUtcDateTime()).Returns(curentDate);

            var workerService = new WorkerService(unitOfWorkMock.Object, clockMock.Object);
            var model = new WorkTimeDTO
            {
                UserId = worker.Id,
                StartTime = new DateTime(2008, 09, 4, 11, 0, 0, DateTimeKind.Utc),
                EndTime = new DateTime(2008, 09, 4, 20, 0, 0, DateTimeKind.Utc)
            };
            // act
            var actual = workerService.AddWorkTime(model);

            // assert
            actual.Should().NotBeNull();
            actual.Succedeed.Should().BeFalse();
        }
        [Test]
        public void AddWorkTime_StartTimeLessEndTime_FalseReturn()
        {
            // arrange
            var worker = new Worker { FirstName = "Ivan", SurName = "Ivanov", Id = 1, Email = "ivanow@email.com", Telephone = "+375251234567" };
            var workers = new List<Worker>();
            workers.AddRange(new Worker[] {
                worker
            });
            var workerManagerMock = new Mock<IWorkerManager>();
            workerManagerMock.Setup(m => m.Get()).Returns(workers);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(m => m.WorkerManager).Returns(workerManagerMock.Object);

            var curentDate = new DateTime(2008, 09, 4, 12, 0, 0, DateTimeKind.Utc);
            var clockMock = new Mock<IClock>();
            clockMock.Setup(m => m.CurentUtcDateTime()).Returns(curentDate);

            var workerService = new WorkerService(unitOfWorkMock.Object, clockMock.Object);
            var model = new WorkTimeDTO
            {
                UserId = worker.Id,
                StartTime = new DateTime(2008, 09, 4, 13, 0, 0, DateTimeKind.Utc),
                EndTime = new DateTime(2008, 09, 4, 12, 0, 0, DateTimeKind.Utc)
            };
            // act
            var actual = workerService.AddWorkTime(model);

            // assert
            actual.Should().NotBeNull();
            actual.Succedeed.Should().BeFalse();
        }
        [Test]
        public void AddWorkTime_NewTimeOverlapsWithAlreadyExistTimeAtStartTime_FalseReturn()
        {
            // arrange
            var worker = new Worker { FirstName = "Ivan", SurName = "Ivanov", Id = 1, Email = "ivanow@email.com", Telephone = "+375251234567" };
            var workers = new List<Worker>();
            workers.AddRange(new Worker[] {
                worker
            });
            var workerManagerMock = new Mock<IWorkerManager>();
            workerManagerMock.Setup(m => m.Get()).Returns(workers);

            var worker1time1 = new WorkTime
            {
                Worker = worker,
                DateStart = new DateTime(2008, 09, 4, 10, 0, 0, DateTimeKind.Unspecified),
                DateEnd = new DateTime(2008, 09, 4, 18, 0, 0, DateTimeKind.Unspecified)
            };
            var workTime = new List<WorkTime>();
            workTime.AddRange(new WorkTime[] {
                worker1time1
            });
            var workTimeManagerMock = new Mock<IWorkTimeManager>();
            workTimeManagerMock.Setup(m => m.Get()).Returns(workTime);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(m => m.WorkerManager).Returns(workerManagerMock.Object);
            unitOfWorkMock.Setup(m => m.WorkTimeManager).Returns(workTimeManagerMock.Object);


            var curentDate = new DateTime(2008, 09, 3, 12, 0, 0, DateTimeKind.Utc);
            var clockMock = new Mock<IClock>();
            clockMock.Setup(m => m.CurentUtcDateTime()).Returns(curentDate);

            var workerService = new WorkerService(unitOfWorkMock.Object, clockMock.Object);
            var model = new WorkTimeDTO
            {
                UserId = worker.Id,
                StartTime = new DateTime(2008, 09, 4, 9, 0, 0, DateTimeKind.Utc),
                EndTime = new DateTime(2008, 09, 4, 11, 0, 0, DateTimeKind.Utc)
            };
            // act
            var actual = workerService.AddWorkTime(model);

            // assert
            actual.Should().NotBeNull();
            actual.Succedeed.Should().BeFalse();
        }
        [Test]
        public void AddWorkTime_NewTimeOverlapsWithAlreadyExistTimeAtEndTime_FalseReturn()
        {
            // arrange
            var worker = new Worker { FirstName = "Ivan", SurName = "Ivanov", Id = 1, Email = "ivanow@email.com", Telephone = "+375251234567" };
            var workers = new List<Worker>();
            workers.AddRange(new Worker[] {
                worker
            });
            var workerManagerMock = new Mock<IWorkerManager>();
            workerManagerMock.Setup(m => m.Get()).Returns(workers);

            var worker1time1 = new WorkTime
            {
                Worker = worker,
                DateStart = new DateTime(2008, 09, 4, 10, 0, 0, DateTimeKind.Unspecified),
                DateEnd = new DateTime(2008, 09, 4, 18, 0, 0, DateTimeKind.Unspecified)
            };
            var workTime = new List<WorkTime>();
            workTime.AddRange(new WorkTime[] {
                worker1time1
            });
            var workTimeManagerMock = new Mock<IWorkTimeManager>();
            workTimeManagerMock.Setup(m => m.Get()).Returns(workTime);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(m => m.WorkerManager).Returns(workerManagerMock.Object);
            unitOfWorkMock.Setup(m => m.WorkTimeManager).Returns(workTimeManagerMock.Object);


            var curentDate = new DateTime(2008, 09, 3, 12, 0, 0, DateTimeKind.Utc);
            var clockMock = new Mock<IClock>();
            clockMock.Setup(m => m.CurentUtcDateTime()).Returns(curentDate);

            var workerService = new WorkerService(unitOfWorkMock.Object, clockMock.Object);
            var model = new WorkTimeDTO
            {
                UserId = worker.Id,
                StartTime = new DateTime(2008, 09, 4, 17, 0, 0, DateTimeKind.Utc),
                EndTime = new DateTime(2008, 09, 4, 19, 0, 0, DateTimeKind.Utc)
            };
            // act
            var actual = workerService.AddWorkTime(model);

            // assert
            actual.Should().NotBeNull();
            actual.Succedeed.Should().BeFalse();
        }
        [Test]
        public void AddWorkTime_NewTimeIsBetweenStartTimeAndEndTime_FalseReturn()
        {
            // arrange
            var worker = new Worker { FirstName = "Ivan", SurName = "Ivanov", Id = 1, Email = "ivanow@email.com", Telephone = "+375251234567" };
            var workers = new List<Worker>();
            workers.AddRange(new Worker[] {
                worker
            });
            var workerManagerMock = new Mock<IWorkerManager>();
            workerManagerMock.Setup(m => m.Get()).Returns(workers);

            var worker1time1 = new WorkTime
            {
                Worker = worker,
                DateStart = new DateTime(2008, 09, 4, 10, 0, 0, DateTimeKind.Unspecified),
                DateEnd = new DateTime(2008, 09, 4, 18, 0, 0, DateTimeKind.Unspecified)
            };
            var workTime = new List<WorkTime>();
            workTime.AddRange(new WorkTime[] {
                worker1time1
            });
            var workTimeManagerMock = new Mock<IWorkTimeManager>();
            workTimeManagerMock.Setup(m => m.Get()).Returns(workTime);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(m => m.WorkerManager).Returns(workerManagerMock.Object);
            unitOfWorkMock.Setup(m => m.WorkTimeManager).Returns(workTimeManagerMock.Object);


            var curentDate = new DateTime(2008, 09, 3, 12, 0, 0, DateTimeKind.Utc);
            var clockMock = new Mock<IClock>();
            clockMock.Setup(m => m.CurentUtcDateTime()).Returns(curentDate);

            var workerService = new WorkerService(unitOfWorkMock.Object, clockMock.Object);
            var model = new WorkTimeDTO
            {
                UserId = worker.Id,
                StartTime = new DateTime(2008, 09, 4, 10, 0, 0, DateTimeKind.Utc),
                EndTime = new DateTime(2008, 09, 4, 18, 0, 0, DateTimeKind.Utc)
            };
            // act
            var actual = workerService.AddWorkTime(model);

            // assert
            actual.Should().NotBeNull();
            actual.Succedeed.Should().BeFalse();
        }
        [Test]
        public void AddWorkTime_StartTimeAndEndTimeIsBetweenNewTime_FalseReturn()
        {
            // arrange
            var worker = new Worker { FirstName = "Ivan", SurName = "Ivanov", Id = 1, Email = "ivanow@email.com", Telephone = "+375251234567" };
            var workers = new List<Worker>();
            workers.AddRange(new Worker[] {
                worker
            });
            var workerManagerMock = new Mock<IWorkerManager>();
            workerManagerMock.Setup(m => m.Get()).Returns(workers);

            var worker1time1 = new WorkTime
            {
                Worker = worker,
                DateStart = new DateTime(2008, 09, 4, 10, 0, 0, DateTimeKind.Unspecified),
                DateEnd = new DateTime(2008, 09, 4, 18, 0, 0, DateTimeKind.Unspecified)
            };
            var workTime = new List<WorkTime>();
            workTime.AddRange(new WorkTime[] {
                worker1time1
            });
            var workTimeManagerMock = new Mock<IWorkTimeManager>();
            workTimeManagerMock.Setup(m => m.Get()).Returns(workTime);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(m => m.WorkerManager).Returns(workerManagerMock.Object);
            unitOfWorkMock.Setup(m => m.WorkTimeManager).Returns(workTimeManagerMock.Object);


            var curentDate = new DateTime(2008, 09, 3, 12, 0, 0, DateTimeKind.Utc);
            var clockMock = new Mock<IClock>();
            clockMock.Setup(m => m.CurentUtcDateTime()).Returns(curentDate);

            var workerService = new WorkerService(unitOfWorkMock.Object, clockMock.Object);
            var model = new WorkTimeDTO
            {
                UserId = worker.Id,
                StartTime = new DateTime(2008, 09, 4, 9, 0, 0, DateTimeKind.Utc),
                EndTime = new DateTime(2008, 09, 4, 19, 0, 0, DateTimeKind.Utc)
            };
            // act
            var actual = workerService.AddWorkTime(model);

            // assert
            actual.Should().NotBeNull();
            actual.Succedeed.Should().BeFalse();
        }
        [Test]
        public void AddWorkTime_NewTimeIfBeforeStartTime_TrueResult()
        {
            // arrange
            var worker = new Worker { FirstName = "Ivan", SurName = "Ivanov", Id = 1, Email = "ivanow@email.com", Telephone = "+375251234567" };
            var workers = new List<Worker>();
            workers.AddRange(new Worker[] {
                worker
            });
            var workerManagerMock = new Mock<IWorkerManager>();
            workerManagerMock.Setup(m => m.Get()).Returns(workers);

            var worker1time1 = new WorkTime
            {
                Worker = worker,
                DateStart = new DateTime(2008, 09, 4, 10, 0, 0, DateTimeKind.Unspecified),
                DateEnd = new DateTime(2008, 09, 4, 18, 0, 0, DateTimeKind.Unspecified)
            };
            var workTime = new List<WorkTime>();
            workTime.AddRange(new WorkTime[] {
                worker1time1
            });
            var workTimeManagerMock = new Mock<IWorkTimeManager>();
            workTimeManagerMock.Setup(m => m.Get()).Returns(workTime);
            workTimeManagerMock.Setup(m => m.Create(It.IsAny<WorkTime>())).Verifiable();

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(m => m.WorkerManager).Returns(workerManagerMock.Object);
            unitOfWorkMock.Setup(m => m.WorkTimeManager).Returns(workTimeManagerMock.Object);


            var curentDate = new DateTime(2008, 09, 3, 12, 0, 0, DateTimeKind.Utc);
            var clockMock = new Mock<IClock>();
            clockMock.Setup(m => m.CurentUtcDateTime()).Returns(curentDate);

            var workerService = new WorkerService(unitOfWorkMock.Object, clockMock.Object);
            var model = new WorkTimeDTO
            {
                UserId = worker.Id,
                StartTime = new DateTime(2008, 09, 4, 9, 0, 0, DateTimeKind.Utc),
                EndTime = new DateTime(2008, 09, 4, 10, 0, 0, DateTimeKind.Utc)
            };
            // act
            var actual = workerService.AddWorkTime(model);

            // assert
            actual.Should().NotBeNull();
            workerManagerMock.VerifyAll();
            actual.Succedeed.Should().BeTrue();
        }
        [Test]
        public void AddWorkTime_NewTimeIfAfterStartTime_TrueResult()
        {
            // arrange
            var worker = new Worker { FirstName = "Ivan", SurName = "Ivanov", Id = 1, Email = "ivanow@email.com", Telephone = "+375251234567" };
            var workers = new List<Worker>();
            workers.AddRange(new Worker[] {
                worker
            });
            var workerManagerMock = new Mock<IWorkerManager>();
            workerManagerMock.Setup(m => m.Get()).Returns(workers);

            var worker1time1 = new WorkTime
            {
                Worker = worker,
                DateStart = new DateTime(2008, 09, 4, 10, 0, 0, DateTimeKind.Unspecified),
                DateEnd = new DateTime(2008, 09, 4, 18, 0, 0, DateTimeKind.Unspecified)
            };
            var workTime = new List<WorkTime>();
            workTime.AddRange(new WorkTime[] {
                worker1time1
            });
            var workTimeManagerMock = new Mock<IWorkTimeManager>();
            workTimeManagerMock.Setup(m => m.Get()).Returns(workTime);
            workTimeManagerMock.Setup(m => m.Create(It.IsAny<WorkTime>())).Verifiable();

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(m => m.WorkerManager).Returns(workerManagerMock.Object);
            unitOfWorkMock.Setup(m => m.WorkTimeManager).Returns(workTimeManagerMock.Object);


            var curentDate = new DateTime(2008, 09, 3, 12, 0, 0, DateTimeKind.Utc);
            var clockMock = new Mock<IClock>();
            clockMock.Setup(m => m.CurentUtcDateTime()).Returns(curentDate);

            var workerService = new WorkerService(unitOfWorkMock.Object, clockMock.Object);
            var model = new WorkTimeDTO
            {
                UserId = worker.Id,
                StartTime = new DateTime(2008, 09, 4, 18, 0, 0, DateTimeKind.Utc),
                EndTime = new DateTime(2008, 09, 4, 19, 0, 0, DateTimeKind.Utc)
            };
            // act
            var actual = workerService.AddWorkTime(model);

            // assert
            actual.Should().NotBeNull();
            workerManagerMock.VerifyAll();
            actual.Succedeed.Should().BeTrue();
        }
    }
}