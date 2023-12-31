﻿Завдання:

На основі отриманого на лекції 7 теоретичного матеріалу скорегувати
програму для лабораторної роботи № 6 наступним чином:
1. В основній програмі додати методи для збереження (серіалізації)
колекції List<T> об’єктів предметної області у файли з форматом *.csv
(*.txt) та *.json, а також методи для читання (десеріалізації) колекції з
відповідних файлів.
*Якщо у класі предметної області реалізований метод ToString() перетворює
об’єкт на рядок, в якому всі значення розділенні комою, то обрати формат *.csv,
інакше – *.txt.


2. Модифікувати меню таким чином (з’являються нові пункти!):
1 – додати об’єкт
2 – вивести на екран об’єкти
3 – знайти об’єкт
4 – видалити об’єкт
5 – демонстрація поведінки об’єктів
6 – демонстрація роботи static методів
7 – зберегти колекцію об’єктів у файлі
8 – зчитати колекцію об’єктів з файлу
9 – очистити колекцію об’єктів
0 – вийти з програми

У пункті меню «7 – зберегти колекцію об’єктів у файлі» необхідно
реалізувати підменю:
1 – зберегти у файл *.csv (*.txt)
2 – зберегти у файл *.json

У пункті меню «8 – зчитати колекцію об’єктів з файлу» необхідно
реалізувати підменю:
1 – зчитати з файлу *.csv (*.txt)
2 – зчитати з файлу *.json


3. Для нових/перероблених методів додати/скорегувати unit-тести.


4. Запустити виконання всіх наявних unit-тестів (як нових, так і з
попередньої лабораторної роботи) і досягти повного їх проходження.


5. Детально протестувати програму. Мають бути протестовані 7-9
пункти меню. При тестуванні десеріалізації перевіряємо процес
перетворення не тільки на коректних файлах *.csv (*.txt) і *.json, а
також не забуваємо перевірити і файли з пропущеними даними і
невірними типами даних.


6. Оформити звіт:
− Титульний аркуш
− Завдання
− Діаграма класів** (**для основного проєкту і тест-проєкту)
− Реалізація класу
− Реалізація тест-класів
− Код програми Program.cs
− Результати запуску всіх unit-тестів
− Результати детального тестування функціональності програми
(навести скріншоти виконання тестування програми або скопіювати і вставити у
звіт вивід програми на екран)