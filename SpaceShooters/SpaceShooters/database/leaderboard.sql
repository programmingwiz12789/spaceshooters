DROP TABLE leaderboard;

CREATE TABLE leaderboard (
    id NUMBER PRIMARY KEY,
    names VARCHAR(255) NOT NULL,
    highscore NUMBER NOT NULL,
    dates VARCHAR(255) NOT NULL
);