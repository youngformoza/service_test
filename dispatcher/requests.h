#ifndef REQUESTS_H
#define REQUESTS_H

#include <QMainWindow>
#include <QDate>
#include "customers.h"
#include "employees.h"
#include "services.h"
#include "status.h"
#include "equipment.h"

using namespace std;

class request
{
public:
    request();
    int id_req;
    QDateTime date_time_start;
    QDateTime date_time_end;
    QString urgency;
    customers cus;
    equipment eq;
    services ser;
    employees recep;
    employees eng;
    status stat;

};

#endif // REQUESTS_H
