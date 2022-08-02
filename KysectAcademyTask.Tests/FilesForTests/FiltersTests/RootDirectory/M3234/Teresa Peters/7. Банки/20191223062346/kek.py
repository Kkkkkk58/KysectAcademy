from account import *
from client import Client
from bank import Bank
from transaction import Transaction


bq = Bank()
bw = Bank(debit_percentage=1.0, credit_fee=50.0,
          credit_limit=40000.0, untrusted_limit=50000.0)

cq = Client('Q', 'QQ', 'AQ123123', 'Lol kek check')
cw = Client('W', 'WW')

bq.clients.append(cq)
bw.clients.append(cw)

aq = Debit(bq, cq)
aw = Credit(bw, cw)

aq += 50000.0
aw += 50000.0

aq.calc_services()
aw.calc_services()
aq.calc_services()
aw.calc_services()
aq.update_services()
aw.update_services()

print(aq.balance, aw.balance)
tr = Transaction(aw, aq, 50000.0)
tr.apply()
print(aq.balance, aw.balance)

aw += 150000.0
print(aq.balance, aw.balance)

try:
    aw -= 150000.0
except Exception as e:
    print(e)
print(aq.balance, aw.balance)

aw -= 50000.0
aw -= 50000.0
aw -= 50000.0

print(aq.balance, aw.balance)
try:
    aw -= 50000.0
except Exception as e:
    print(e)
print(aq.balance, aw.balance)

tr.rollback()
print(aq.balance, aw.balance)

print('done')
