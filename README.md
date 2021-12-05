# MeDirect Lights Out Puzzle

1-)Firstly, you have to create database and table with script file which is name "MeDirectGame.sql" 
2-)Secondly, you have to check to connection String which is in the MeDirect.Api project "appsettings.json" file. 
3-)The Server Name should be same your pc  "Mssql Server Name".
4-)Build and Start Api and Web Project with together.


You can play game on "Home" Page.
if you want to change game settings you can use Settings Page which is link on the Header Bar

The Application has unit test which is design by EntityFramework Core In Memory Database.
Therefore unit test should is worked with "Run All". 
if you want to try one by one , you dont forget database is "in memory" and you can manage to the data with json file which is in the Test project "data" folder.
