using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CMCS
{
    internal class Program
    { // Simulate the database
        static List<Lecturer> lecturers = new List<Lecturer>
        {
            new Lecturer { LecturerId = 1, Name = "Simpiwe Mbandazayo" },
            new Lecturer { LecturerId = 2, Name = "Cherrone Walker" }
        };
        static void Main(string[] args)
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("Contract Monthly Claim System");
                Console.WriteLine("1. Lecturer - Submit Claim");
                Console.WriteLine("2. Admin - Approve/Reject Claims");
                Console.WriteLine("3. Exit");
                Console.Write("Choose an option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        LecturerMenu();
                        break;
                    case "2":
                        AdminMenu();
                        break;
                    case "3":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
            }
        }

        static void LecturerMenu()
        {
            Console.Clear();
            Console.WriteLine("Lecturer Menu");
            Console.Write("Enter your lecturer ID: ");
            int lecturerId = int.Parse(Console.ReadLine());

            var lecturer = lecturers.FirstOrDefault(l => l.LecturerId == lecturerId);
            if (lecturer == null)
            {
                Console.WriteLine("Lecturer not found!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"Welcome {lecturer.Name}");
            Console.WriteLine("1. Submit a new claim");
            Console.WriteLine("2. View claim history");
            Console.WriteLine("3. Go back");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    SubmitClaim(lecturer);
                    break;
                case "2":
                    ViewClaimHistory(lecturer);
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }

        static void SubmitClaim(Lecturer lecturer)
        {
            Console.Clear();
            Console.WriteLine("Submit a new claim");
            Console.Write("Date Worked (yyyy-mm-dd): ");
            DateTime dateWorked = DateTime.Parse(Console.ReadLine());
            Console.Write("Hours Worked: ");
            double hoursWorked = double.Parse(Console.ReadLine());
            Console.Write("Hourly Rate: ");
            double hourlyRate = double.Parse(Console.ReadLine());
            Console.Write("Comment (Optional): ");
            string comment = Console.ReadLine();
            Console.Write("Path to Supporting Document (e.g., C:\\file.pdf): ");
            string documentPath = Console.ReadLine();

            var claim = new Claim
            {
                ClaimId = lecturer.Claims.Count + 1,
                LecturerId = lecturer.LecturerId,
                DateWorked = dateWorked,
                HoursWorked = hoursWorked,
                HourlyRate = hourlyRate,
                Comment = comment,
                SupportingDocumentPath = documentPath,
                Status = ClaimStatus.Pending
            };

            lecturer.Claims.Add(claim);

            Console.WriteLine("Claim Submitted Successfully!");
            Console.ReadKey();
        }

        static void ViewClaimHistory(Lecturer lecturer)
        {
            Console.Clear();
            Console.WriteLine("Claim History");
            foreach (var claim in lecturer.Claims)
            {
                Console.WriteLine($"Claim ID: {claim.ClaimId}, Date Worked: {claim.DateWorked.ToShortDateString()}, Total: {claim.TotalAmount:C}, Status: {claim.Status}");
                if (!string.IsNullOrEmpty(claim.SupportingDocumentPath))
                {
                    Console.WriteLine($"Supporting Document: {claim.SupportingDocumentPath}");
                }
            }

            Console.ReadKey();
        }

        static void AdminMenu()
        {
            Console.Clear();
            Console.WriteLine("Admin Menu");

            var pendingClaims = lecturers.SelectMany(l => l.Claims).Where(c => c.Status == ClaimStatus.Pending).ToList();

            if (!pendingClaims.Any())
            {
                Console.WriteLine("No pending claims.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Pending Claims");
            foreach (var claim in pendingClaims)
            {
                Console.WriteLine($"Claim ID: {claim.ClaimId}, Lecturer: {lecturers.FirstOrDefault(l => l.LecturerId == claim.LecturerId).Name}, Date Worked: {claim.DateWorked.ToShortDateString()}, Total: {claim.TotalAmount:C}");
            }

            Console.Write("Enter Claim ID to approve/reject: ");
            int claimId = int.Parse(Console.ReadLine());
            var selectedClaim = pendingClaims.FirstOrDefault(c => c.ClaimId == claimId);
            if (selectedClaim == null)
            {
                Console.WriteLine("Claim not found!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("1. Approve Claim");
            Console.WriteLine("2. Reject Claim");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    selectedClaim.Status = ClaimStatus.Approved;
                    Console.WriteLine("Claim approved.");
                    break;
                case "2":
                    selectedClaim.Status = ClaimStatus.Rejected;
                    Console.WriteLine("Claim rejected.");
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }

            Console.ReadKey();
        }
    }
}
