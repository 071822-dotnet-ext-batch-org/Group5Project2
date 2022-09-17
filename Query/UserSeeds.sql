DELETE FROM Profiles;

INSERT INTO Profiles (profileName, profileAddress, profilePhone, profileEmail, fk_userID, profilePicture)
VALUES ('John Wick', '123 Arroyo Lane', '123-456-7890', 'mydogisdead@email.com', 'auth0test1', 'http://placekitten.com/200/300');

INSERT INTO Profiles (profileName, profileAddress, profilePhone, profileEmail, fk_userID, profilePicture)
VALUES ('Batman', 'The Batcave', '837-384-3948', 'thebatman@email.com', 'auth0test2', 'http://placekitten.com/200/300');

INSERT INTO Profiles (profileName, profileAddress, profilePhone, profileEmail, fk_userID, profilePicture)
VALUES ('Omar Little', 'Balmore', '649-968-6523', 'omarcoming@email.com', 'auth0test3', 'http://placekitten.com/200/300');

