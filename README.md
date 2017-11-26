# ExagoNLP

## Table of Contents
* Proposal
* Goals/Deliverables
* Minimal Requirements

## Proposal
Exago Business Intelligence is a software application that allows users to easily create their own reports and visualizations, and do analysis on the data that is being accessed.  A natural extension to this is NLP, where a user can ‘ask’ a question in either typed or voice input, and get an answer back based on the data in either displayed or voice (or potentially others such as SMS, email, etc.) output. 

We as humans can understand what the person is requesting, but how can we write computer algorithms that can make these conclusions?  If the computer can't understand ore receives an ambiguous request, can we build an AI that asks the user for more specific information, then ‘learn’ from the response going forward?  How do we use the names of data objects (e.g. tables, views) and field names to help make educated conclusions about what the user is asking?
 
The purpose of this project is to interpret what the user is asking, form a query against the database, and return a result.  If there is uncertainty in the question, then ask a question back to the user and learn the answer going forward.  

We are currently planning on using the freely-available Northwind database for development purposes as per recommendation from our Exago contacts, as it is well structured and used commonly within their organization. Other databases may be used for testing; we’ll look to ensure that there are no references in the code to any specific database.

## Goals/Deliverables
* Accessing and understanding NLP technology (9/31)
* Form a cohesive understanding of linguistics (10/15)
* Integrate NLP software to Bot (12/3)
* Process the NLP data to generate a SQL query on the database (12/15)
* Add voice recognition to Microsoft Bot Framework (Possibly next semester)

## Minimal Requirements
### Microsoft Bot Framework Emulator
#### Ubuntu
* Run the bot: `node app.js` (In the bot directory)
* Build from [source](https://github.com/Microsoft/BotFramework-Emulator.git)
* Install the Node packages (this will take a while):
`npm install`
* Build the Bot Emulator: `npm run build`
* Run the Bot Emulator: `npm run start`
* Set the endpoint to `http://localhost:3978/api/messages` -- this is the default debugging endpoint for a bot running on localhost
* Type in input
### MySQL-Python Connector:
#### Windows 10
* Install [Python 3.4]((https://www.python.org/downloads/release/python-340/) for Windows 10 for all users
* Add the location of this application to your system's PATH
* Install [MySQL driver](https://dev.mysql.com/downloads/connector/python/)

### To generate a Northwind database in MySQL from script:
*Install the [MySQL application](https://dev.mysql.com/downloads/installer/) (stick with default settings, and configure as a development machine)

* Download the [script](https://www.aspsnippets.com/Articles/Download-and-Install-Microsoft-Northwind-Sample-database-in-MySql.aspx) to build a Northwind Database (select Northwind.MySQL5.sql)
* Open script in a text editor, copy the contents to the MySQL instance and execute
* *There's a small SQL syntax error on line 4601, the line GROUP BY City WITH ROLLUP;
to fix it just remove WITH ROLLUP and the script should execute fully

* To verify the database has been built properly, open a new tab for querying and run a small query (ex):
USE northwind;
SHOW tables;
SHOW columns FROM employees;

* (Should generate two tables as results, with all database tables
   listed in the first and each employee entry columns in the second)