# API Endpoints

## Auth
- `POST /api/auth/register`
- `POST /api/auth/login`

## Student
- `GET /api/students/profile/{studentProfileId}`
- `GET /api/students/profile/user/{userId}`
- `GET /api/students/{studentProfileId}/badges`
- `GET /api/students/{studentProfileId}/answers`
- `GET /api/students/{studentProfileId}/quiz-result/{quizId}`
- `POST /api/students/submit-answer`

## Quiz
- `GET /api/quizzes?pageNumber=1&pageSize=10`
- `GET /api/quizzes/{id}`
- `GET /api/quizzes/level/{level}`
- `POST /api/quizzes`
- `PUT /api/quizzes/{id}`
- `DELETE /api/quizzes/{id}`

## Question
- `GET /api/questions/quiz/{quizId}`
- `GET /api/questions/{id}`
- `POST /api/questions`
- `PUT /api/questions/{id}`
- `DELETE /api/questions/{id}`

## Answer
- `GET /api/answers/question/{questionId}`
- `GET /api/answers/{id}`
- `POST /api/answers`
- `PUT /api/answers/{id}`
- `DELETE /api/answers/{id}`

## Video
- `GET /api/videos?pageNumber=1&pageSize=10`
- `GET /api/videos/{id}`
- `POST /api/videos`
- `PUT /api/videos/{id}`
- `DELETE /api/videos/{id}`

## Safety Tip
- `GET /api/safety-tips?pageNumber=1&pageSize=10`
- `GET /api/safety-tips/{id}`
- `POST /api/safety-tips`
- `PUT /api/safety-tips/{id}`
- `DELETE /api/safety-tips/{id}`

## Parking Zone
- `GET /api/parking-zones?pageNumber=1&pageSize=10`
- `GET /api/parking-zones/{id}`
- `POST /api/parking-zones`
- `PUT /api/parking-zones/{id}`
- `DELETE /api/parking-zones/{id}`

## Badge
- `GET /api/badges`
- `GET /api/badges/{id}`
- `POST /api/badges`
- `PUT /api/badges/{id}`
- `DELETE /api/badges/{id}`

## Dashboard
- `GET /api/dashboard/stats`
