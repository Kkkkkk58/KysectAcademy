from Builder import PCBuilder, PCManualBuilder, Director
import PC


def main():
    director = Director()
    pcb: PCBuilder = PCBuilder()
    manb: PCManualBuilder = PCManualBuilder()

    print('If it can works with Directors, it will be work and without them, so here are two variables of PC:')
    print('\n\nEconomy PC:\n')
    director.builder(pcb)
    director.economy()
    pc: PC.PortableComputer = pcb.get_result()
    director.builder(manb)
    director.economy()
    pc_manual: PC.PCManual = manb.get_result()
    pc_manual.print_manual()

    print('\n\nTOP PC:\n')
    director.builder(pcb)
    director.top()
    pc: PC.PortableComputer = pcb.get_result()
    director.builder(manb)
    director.top()
    pc_manual: PC.PCManual = manb.get_result()
    pc_manual.print_manual()


if __name__ == '__main__':
    main()

'''
Необходимо реализовать логику создания (сборки) системного блока персонального компьютера. Системный блок имеет:
1.  Материнскую плату (модель и производитель, можно сделать типом string) - обязательный элемент
2.  Процессор (модель+производитель (string), тактовая частота (double)) - обязательный элемент
3.  Хранилище данных (тип - жёсткий диск/SSD (лучше использовать enum)
    и производитель+марка (тут можно string) - обязательный элемент
4.  Оперативная память (тип - DDR3/DDR4(enum), производитель+марка(string), ёмкость(int) - обязательный элемент.
    Учтите, что может стоять несколько плашек.
5.  Видеокарта - модель+производитель, string - опциональный элемент
6.  Жидкостное охлаждение - модель+производитель, string - опциональный элемент.
Кроме того, нужно также реализовать создание документации по собранному системному блоку,
которая бы отражала его элементы (это должен быть отдельный класс).
При этом есть стандартные конфигурации компьютеров, составные элементы которых заранее известны
(эконом, средний, комфорт, топовый). Конкретные значения для элементов и конфигураций можете придумать сами
'''
