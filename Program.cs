namespace SimpleHotelRoomManagementProject_CSharpProject2
{
    internal class Program
    {
        static int[] RoomNumbers = new int[10];
        static double[] RoomRates = new double[10];
        static bool[] isReserved = new bool[10];
        static string[] GuestNames = new string[10];
        static int[] nights = new int[10];
        static DateTime[] bookingDates = new DateTime[10];
        static int roomCount = 0;
        static int MAX_ROOMS = 10;


        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\nSelect a Program:");
                Console.WriteLine("1. Add a new room");
                Console.WriteLine("2. View all rooms");
                Console.WriteLine("3. Reserve a room for a guest");
                Console.WriteLine("4. View all reservations with total cost");
                Console.WriteLine("5. Search reservation by guest name");
                Console.WriteLine("6. Find the highest-paying guest");
                Console.WriteLine("7. Cancel a reservation by room number");
                Console.WriteLine("8. Exit");

                Console.Write("Enter your choice: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1: AddANewRoom(); break;
                    case 2: ViewAllRooms(); break;
                    case 3: ReserveARoomForAGuest(); break;
                    case 4: ViewAllReservationsWithTotalCost(); break;
                    case 5: SearchReservationByGuestName(); break;
                    case 6: FindTheHighestPayingGuest(); break;
                    case 7: CancelAReservationByRoomNumber(); break;
                    case 8: return;
                    default: Console.WriteLine("Invalid Choice! Try again."); break;
                }
                Console.ReadLine();
            }
            // Add A New Room
            static void AddANewRoom()
            {
                char ChoiceChar = 'y';
                while (roomCount < MAX_ROOMS)
                {
                    try
                    {
                        Console.WriteLine("Enter Room Number:");
                        RoomNumbers[roomCount] = int.Parse(Console.ReadLine());


                        Console.WriteLine("Enter the daily rate for room" + RoomNumbers[roomCount]);
                        RoomRates[roomCount] = double.Parse(Console.ReadLine());


                        isReserved[roomCount] = false;
                        GuestNames[roomCount] = "";
                        nights[roomCount] = 0;
                        bookingDates[roomCount] = DateTime.Now;


                        roomCount++;
                        Console.WriteLine("room Added Successfully");

                        Console.WriteLine("Do you want add another room ? y / n");
                        ChoiceChar = Console.ReadKey().KeyChar;
                        Console.WriteLine();
                        if (ChoiceChar != 'y' && ChoiceChar != 'Y')
                            break;
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine("Invalid input. Please enter a valid number."); // Handling input errors 
                    }
                }
                if (roomCount == MAX_ROOMS)
                    Console.WriteLine("Cannot add more there are no room. Maximum limit reached.");
            }
            // View All Room
            static void ViewAllRooms()
            {
                try
                {
                    if (roomCount == 0)
                    {
                        Console.WriteLine("No rooms available.");
                        return;
                    }

                    Console.WriteLine("Room Details:");
                    for (int i = 0; i < roomCount; i++)
                    {
                        Console.WriteLine($"Room {RoomNumbers[i]}, {RoomRates[i]} per night");
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("An error occurred: Room data out of range."); // Handling input errors 
                }
                catch (Exception ex)
                {
                    // This will catch any other unforeseen errors
                    Console.WriteLine("An unexpected error occurred.");
                }
            }
            // Reserve A Room For A Guest
            static void ReserveARoomForAGuest()
            {
                try
                {
                    if (roomCount == 0)
                    {
                        Console.WriteLine("No rooms available for reservation.");
                        return;
                    }

                    Console.Write("Enter the room number to reserve: ");
                    int roomNumber = int.Parse(Console.ReadLine());

                    // Check if the room exists and is available
                    int roomIndex = Array.IndexOf(RoomNumbers, roomNumber);
                    if (roomIndex == -1)
                    {
                        Console.WriteLine("Room number not found.");
                    }
                    else if (isReserved[roomIndex])
                    {
                        Console.WriteLine("Room is already reserved.");
                    }
                    else
                    {
                        Console.Write("Enter guest name: ");
                        GuestNames[roomIndex] = Console.ReadLine();

                        Console.Write("Enter number of nights: ");
                        nights[roomIndex] = int.Parse(Console.ReadLine());

                        bookingDates[roomIndex] = DateTime.Now;
                        isReserved[roomIndex] = true;

                        Console.WriteLine($"Room {roomNumber} reserved successfully for {GuestNames[roomIndex]}.");
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Invalid input. Please enter a valid number for room number or number of nights.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An unexpected error occurred.");
                }
            }
            // View All Reservations With Total Cost
            static void ViewAllReservationsWithTotalCost()
            {
                try
                {
                    bool anyReservations = false;

                    for (int i = 0; i < roomCount; i++)
                    {
                        if (isReserved[i])
                        {
                            anyReservations = true;
                            Console.WriteLine($"Room {RoomNumbers[i]} reserved by {GuestNames[i]} for {nights[i]} nights, booked on {bookingDates[i]:d}.");
                        }
                    }

                    if (!anyReservations)
                    {
                        Console.WriteLine("No rooms have been reserved.");
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(" Error: There was a problem accessing the room reservation data.");
                }
            }
            // Search Reservation By Guest Name 
            static void SearchReservationByGuestName()
            {
                try
                {
                    Console.Write("Enter guest name to search: ");
                    string guestName = Console.ReadLine().ToLower();

                    bool IsFound = false;
                    for (int i = 0; i < roomCount; i++)
                    {
                        if (isReserved[i] && GuestNames[i].ToLower().Contains(guestName))
                        {
                            Console.WriteLine($"Room Number: {RoomNumbers[i]}, Guest Name: {GuestNames[i]}, Nights: {nights[i]}");
                            IsFound = true;
                        }
                    }

                    if (!IsFound)
                    {
                        Console.WriteLine("No reservation found for this guest.");
                    }
                }
                catch (NullReferenceException ex)
                {
                    // This will catch any null reference issues 
                    Console.WriteLine("Error: One of the required data arrays is not initialized.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An unexpected error occurred.");
                }
            }
            // Find The Highest Paying Guest
            static void FindTheHighestPayingGuest()
            {
                try
                {
                    double maxAmount = 0;
                    string highestPayingGuest = "";

                    for (int i = 0; i < roomCount; i++)
                    {
                        if (isReserved[i])
                        {
                            double totalCost = nights[i] * RoomRates[i];
                            if (totalCost > maxAmount)
                            {
                                maxAmount = totalCost;
                                highestPayingGuest = GuestNames[i];
                            }
                        }
                    }

                    if (string.IsNullOrEmpty(highestPayingGuest))
                    {
                        Console.WriteLine("No reservations to check.");
                    }
                    else
                    {
                        Console.WriteLine($"Highest paying guest: {highestPayingGuest} with total cost of {maxAmount}");
                    }
                }
                catch (IndexOutOfRangeException ex)
                {
                    // This will catch any indexing issues if roomCount exceeds array sizes or similar errors
                    Console.WriteLine("Error: Room count exceeds the available data arrays.");
                }
                catch (NullReferenceException ex)
                {
                    // This will catch any null reference issues 
                    Console.WriteLine("Error: One of the required data arrays is not initialized.");

                }
                catch (InvalidOperationException ex)
                {
                    // Handles cases where there's invalid data 
                    Console.WriteLine("Error: Invalid data encountered.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An unexpected error occurred.");
                }
            }
            // Cancel A Reservation By Room Number
            static void CancelAReservationByRoomNumber()
            {
                try
                {
                    Console.Write("Enter room number to cancel reservation: ");
                    int roomNumber = int.Parse(Console.ReadLine());

                    int roomIndex = Array.IndexOf(RoomNumbers, roomNumber);
                    if (roomIndex == -1 || !isReserved[roomIndex])
                    {
                        Console.WriteLine("Room is either invalid or not reserved.");
                        return;
                    }

                    // Cancel reservation
                    isReserved[roomIndex] = false;
                    GuestNames[roomIndex] = "";
                    nights[roomIndex] = 0;
                    bookingDates[roomIndex] = DateTime.MinValue;

                    Console.WriteLine("Reservation cancelled successfully.");
                }
                catch (FormatException ex)
                {
                    // Handles invalid input 
                    Console.WriteLine("Error: Invalid input. Please enter a valid room number.");
                }
                catch (IndexOutOfRangeException ex)
                {
                    // Handles indexing errors if arrays are not aligned properly or room count exceeds array size
                    Console.WriteLine("Error: Room number out of range.");
                }
                catch (NullReferenceException ex)
                {
                    // Handles issues if any of the arrays are null or not initialized
                    Console.WriteLine("Error: One or more required data arrays are not initialized.");
                }
                catch (Exception ex)
                {
                    // General exception handler for any unforeseen errors
                    Console.WriteLine("An unexpected error occurred.");
                }
            }
        }
    }
}


