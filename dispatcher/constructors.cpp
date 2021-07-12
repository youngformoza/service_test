#include "customers.h"
#include "employees.h"
#include "services.h"
#include "status.h"
#include "equipment.h"
#include "positions.h"
#include "equipment_class.h"
#include "vendor.h"
#include "requests.h"

#include <iostream>
#include <QDate>
#include <QDateTime>
using namespace std;



customers::customers()
{
    this -> id_cus=-1;
    this->name="";
    this -> position = "";
    this -> birthday.setDate(1900, 01, 01);
    this -> mail = "";
    this -> phone = -1;

}

employees::employees()
{
    this->id_empl=-1;
    this->name="";
    this->qualification = "";
    this->pos=positions();
}

positions::positions()
{
    this->id_pos=-1;
    this->name="";
}

equipment::equipment()
{
    this->id_eq=-1;
    this->series = "";
    this->eq_cl = equipment_class();
    this->ven=vendor();
}

equipment_class::equipment_class()
{
    this->id_eq_cl=-1;
    this->name="";
}

vendor::vendor()
{
    this->id_ven=-1;
    this->name="";
}

request::request()
{
    this->id_req=-1;
    this->date_time_start.currentDateTime();
    this->date_time_end.isNull();
    this->urgency = "";
    this->cus=customers();
    this->eq = equipment();
    this->ser = services();
    this->recep = employees();
    this->eng=employees();
    this->stat=status();
}

status::status()
{
    this->id_stat=-1;
    this->name = "";
}

services::services()
{
    this->id_ser=-1;
    this->name="";
}
