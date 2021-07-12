#ifndef EMPLOYEES_H
#define EMPLOYEES_H

#include <QMainWindow>
#include "positions.h"
using namespace std;

class employees
{
public:
    employees();
    int id_empl;
    QString name;
    QString qualification;
    positions pos;


};

#endif // EMPLOYEES_H
