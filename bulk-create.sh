maxAttempts=$1
for i in `seq 1 $maxAttempts`; do
  curl -X 'POST' 'http://localhost:5153/appointment' -H 'accept: */*'  -H 'Content-Type: application/json' -d '{ "bookingTime": "09:00", "name": "person" }' -w '\n'
done