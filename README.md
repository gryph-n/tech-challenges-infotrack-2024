# Booking API
Simple API built with Clean Architecture and xUnit testing

## Prerequisites
- .NET 8 SDK

## Running locally
`dotnet run --project InfoTrack.Booking.Api`

On successful run, Swagger will open in a web-browser for GUI interaction with the API

## Bulk insertion
To insert many entries quickly to test `409` result, you can use the included bash script and run it with the below commands
```bash
chmod -755 ./bulk-create.sh;
./bulk-create.sh 5
```

Or simply run the below command multiple times in a bash terminal
```bash
curl -X 'POST' 'http://localhost:5153/appointment' -H 'accept: */*'  -H 'Content-Type: application/json' -d '{ "bookingTime": "09:00", "name": "person" }' -w '\n'
```

## Original technical requirement
- Assume that all bookings are for the same day (do not worry about handling dates)
- InfoTrack's hours of business are 9am-5pm, all bookings must complete by 5pm (latest booking
is 4:00pm)
- Bookings are for 1 hour (booking at 9:00am means the spot is held from 9:00am to 9:59am)
- InfoTrack accepts up to 4 simultaneous settlements
- API needs to accept POST requests of the following format:
```json
{
  "bookingTime": "09:30",
  "name":"John Smith"
}
```
- Successful bookings should respond with an OK status and a booking Id in GUID form
```json
{
  "bookingId": "d90f8c55-90a5-4537-a99d-c68242a6012b"
}
```
- Requests for out of hours times should return a Bad Request status
- Requests with invalid data should return a Bad Request status
- Requests when all settlements at a booking time are reserved should return a Conflict status
- The name property should be a non-empty string
- The bookingTime property should be a 24-hour time (00:00 - 23:59)
- Bookings can be stored in-memory, it is fine for them to be forgotten when the application is
restarted