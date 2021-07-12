#ifndef EQUIPMENT_H
#define EQUIPMENT_H

#include <QMainWindow>
#include "equipment_class.h"
#include "vendor.h"

using namespace std;

class equipment
{
public:
    equipment();
    int id_eq;
    QString series;
    equipment_class eq_cl;
    vendor ven;

};



#endif // EQUIPMENT_H
