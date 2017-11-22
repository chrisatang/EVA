import datetime

import mysql.connector
from mysql.connector import errorcode

try:
    cnx = mysql.connector.connect(user='root', password='eva_develop',
                                        host='127.0.0.1',
                                        database='northwind')
except mysql.connector.Error as err:
  if err.errno == errorcode.ER_ACCESS_DENIED_ERROR:
    print("Something is wrong with user name or password")
  elif err.errno == errorcode.ER_BAD_DB_ERROR:
    print("Database does not exist")
  else:
    print(err)

else:
    cursor = cnx.cursor()
    cmd = 3

    q1 = ("SHOW tables")
    q2 = ("SHOW columns FROM employees;")
    q3 = ("SELECT c.CustomerID,c.City FROM customers c WHERE c.City = 'Boise'")

    if(cmd == 1):
      query = q1
    if(cmd == 2):
      query = q2
    if(cmd == 3):
      query = q3
    else:
      query = q1

    cursor.execute(query)
    for col in cursor:
        print(col,'\n')
    # query = ("SELECT first_name, last_name, hire_date FROM employees "
    #      "WHERE hire_date BETWEEN %s AND %s")

    # hire_start = datetime.date(1999, 1, 1)
    # hire_end = datetime.date(1999, 12, 31)

    # cursor.execute(query, (hire_start, hire_end))

    # for (first_name, last_name, hire_date) in cursor:
    #     print("{}, {} was hired on {:%d %b %Y}".format(last_name, first_name, hire_date))

    cursor.close()
    cnx.close()