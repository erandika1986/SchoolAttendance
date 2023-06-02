using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolAttendance.Domain.Entities;


namespace SchoolAttendance.Infrastructure.Data
{
    public class ApplicationDbContextInitialiser
    {
        private readonly ILogger<ApplicationDbContextInitialiser> _logger;
        private readonly SchoolAttendanceContext _context;

        public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, SchoolAttendanceContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                if (_context.Database.IsMySql())
                {
                    await _context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        public async Task TrySeedAsync()
        {
            // Default roles
            var administratorRole = new Role()
            {
                Name = "Admin"
            };

            var principleRole = new Role()
            {
                Name = "Principle"
            };

            var vicePrincipleRole = new Role()
            {
                Name = "VicePrinciple"
            };

            var levelHeadRole = new Role()
            {
                Name = "LevelHead"
            };

            var departmentHeadRole = new Role()
            {
                Name = "DepartmentHead"
            };

            var teacherRole = new Role()
            {
                Name = "Teacher"
            };

            var studentRole = new Role()
            {
                Name = "Student"
            };

            var parentRole = new Role()
            {
                Name = "Parent"
            };

            if (_context.Roles.All(r => r.Name != administratorRole.Name))
            {
                await _context.Roles.AddAsync(administratorRole);
            }

            if (_context.Roles.All(r => r.Name != principleRole.Name))
            {
                await _context.Roles.AddAsync(principleRole);
            }

            if (_context.Roles.All(r => r.Name != vicePrincipleRole.Name))
            {
                await _context.Roles.AddAsync(vicePrincipleRole);
            }

            if (_context.Roles.All(r => r.Name != levelHeadRole.Name))
            {
                await _context.Roles.AddAsync(levelHeadRole);
            }

            if (_context.Roles.All(r => r.Name != departmentHeadRole.Name))
            {
                await _context.Roles.AddAsync(departmentHeadRole);
            }

            if (_context.Roles.All(r => r.Name != teacherRole.Name))
            {
                await _context.Roles.AddAsync(teacherRole);
            }

            if (_context.Roles.All(r => r.Name != studentRole.Name))
            {
                await _context.Roles.AddAsync(studentRole);
            }

            if (_context.Roles.All(r => r.Name != parentRole.Name))
            {
                await _context.Roles.AddAsync(parentRole);
            }

            // Default users
            var administrator = new User 
            { 
                Username = "admin", 
                Email = "admin@gmail.com",
                FullName = "Admin",
                Gender = "M",
                IsActive = true,
                TimeZoneId = "Sri Lanka Standard Time",
                ContactNo ="+94702605650",
                Password = BCrypt.Net.BCrypt.HashPassword("Pass@123!")
            };

            administrator.UserRoles = new List<UserRole>();

            administrator.UserRoles.Add(new UserRole() { Role = administratorRole });

            if (_context.Users.All(u => u.Username != administrator.Username))
            {
                await _context.Users.AddAsync(administrator);
            }


            await AddAcademicYears();

            await AddAssessmentType();

            await AddDays();

            await _context.SaveChangesAsync();
        }

        private async Task AddAcademicYears()
        {
            var academicYear2022 = new AcademicYear()
            {
                Id = 2022,
                Name = "2022",
                IsCurrentYear = true,
            };

            if (_context.AcademicYears.All(a => a.Id != academicYear2022.Id))
            {
                await _context.AcademicYears.AddAsync(academicYear2022);
            }
        }
        private async Task AddAssessmentType()
        {
            var unitTest = new AssessmentType()
            {
                Name = "Unit Test",
            };

            var monthlyTest = new AssessmentType()
            {
                Name = "Monthly Test",
            };

            var firstTermTest = new AssessmentType()
            {
                Name = "First Term Test",
            };

            var secondTermTest = new AssessmentType()
            {
                Name = "Second Term Test",
            };

            var thirdTermTest = new AssessmentType()
            {
                Name = "Third Term Test",
            };

            var assignment = new AssessmentType()
            {
                Name = "Assignment",
            };

            if (_context.AssessmentTypes.All(a => a.Name != unitTest.Name))
            {
                await _context.AssessmentTypes.AddAsync(unitTest);
            }
            if (_context.AssessmentTypes.All(a => a.Name != monthlyTest.Name))
            {
                await _context.AssessmentTypes.AddAsync(monthlyTest);
            }
            if (_context.AssessmentTypes.All(a => a.Name != firstTermTest.Name))
            {
                await _context.AssessmentTypes.AddAsync(firstTermTest);
            }
            if (_context.AssessmentTypes.All(a => a.Name != secondTermTest.Name))
            {
                await _context.AssessmentTypes.AddAsync(secondTermTest);
            }

            if (_context.AssessmentTypes.All(a => a.Name != thirdTermTest.Name))
            {
                await _context.AssessmentTypes.AddAsync(thirdTermTest);
            }

            if (_context.AssessmentTypes.All(a => a.Name != assignment.Name))
            {
                await _context.AssessmentTypes.AddAsync(assignment);
            }
        }
        private async Task AddDays()
        {
            if (!_context.Days.Any())
            {
                var sunday = new Day()
                {
                    Id = 1,
                    Name = "Sunday"
                };

                var monday = new Day()
                {
                    Id = 2,
                    Name = "Monday"
                };

                var tuesday = new Day()
                {
                    Id = 3,
                    Name = "Tuesday"
                };

                var wednesday = new Day()
                {
                    Id = 4,
                    Name = "Wednesday"
                };

                var thursday = new Day()
                {
                    Id = 5,
                    Name = "Thursday"
                };

                var friday = new Day()
                {
                    Id = 6,
                    Name = "Friday"
                };

                var saturday = new Day()
                {
                    Id = 7,
                    Name = "Saturday"
                };


                if (_context.Days.All(a => a.Id != sunday.Id))
                {
                    await _context.Days.AddAsync(sunday);
                }

                if (_context.Days.All(a => a.Id != monday.Id))
                {
                    await _context.Days.AddAsync(monday);
                }

                if (_context.Days.All(a => a.Id != tuesday.Id))
                {
                    await _context.Days.AddAsync(tuesday);
                }

                if (_context.Days.All(a => a.Id != wednesday.Id))
                {
                    await _context.Days.AddAsync(wednesday);
                }

                if (_context.Days.All(a => a.Id != thursday.Id))
                {
                    await _context.Days.AddAsync(thursday);
                }

                if (_context.Days.All(a => a.Id != friday.Id))
                {
                    await _context.Days.AddAsync(friday);
                }

                if (_context.Days.All(a => a.Id != saturday.Id))
                {
                    await _context.Days.AddAsync(saturday);
                }
            }
        }
    }
}
