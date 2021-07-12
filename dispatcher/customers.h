#ifndef CUSTOMERS_H
#define CUSTOMERS_H

#include <QMainWindow>
#include <QDate>
using namespace std;

class customers
{
public:
    customers();
    int id_cus;
    QString name;
    QString position;
    QDate birthday;
    QString mail;
    long long phone;

};


#endif // CUSTOMERS_H
