Не судите строго. Это мой первый большой PET-проект.Я его начал делать чтобы самому понять, чему я научился за год.
В нем я попытался применить те знания которые получил.Как оказалось самое сложное было, это спроектировать его.

Он состоит из 5 проектов.

  1) WebApi- это сердце. Он связывает все остальные проекты с базой данных. В нем реализована аутентификация и авторизация с
  помощью JWT токена и ролей.
  Ссылка http://jobaspapi-001-site1.gtempurl.com/swagger/index.html
  
  2) Сайт- тут можно заказать пиццу.Он сделан так, что можно легко добавить новый товар(На добавление KingSize пиццы ушло 3 часа).
  В нем нет аутентификация, он создает заказ на основе выбора человека. Отдельно сохраняет данные о клиенте.
  Ссылка http://diplompizza-001-site1.htempurl.com/
  
  3) Директор- тут есть аутентификация и авторизация. Только он может нанимать(создавать) и назначать должность(роль).Еще может
  уволить(удалить),изменить данные о сотруднике,назначать другую должность(роль).Также ему в режиме онлайн приходит информация 
  о каждом заказе, на каком заказ этапе, кто его делает.В штате есть два Managers,два Cooks, один Administrator.
  Ссылка http://director.somee.com/
  
  4) Кухня - сюда в режиме онлайн приходят только те заказы, которые обработал Manager. Их видно без аутентификации.Но чтобы взять в 
  работу,нужно войти в приложение. Когда один повар берет заказ, другой уже взять не может.Как только повар возьмет заказ, у 
  директора это отображается.
  Ссылка http://Kitchen.somee.com
  
  5) Офис-сюда в режиме онлайн приходят все заказы,с ними работает Manager.Как только клиент заказал.У него сразу  появляется 
  заказ и желтый флажок.Manager должен нажать кнопку с карандашом и уточнить все у клиента.Вернуться назад и нажать кнопку с зеленым
  флажком.После этого заказ появится на кухне.Как только повар возьмет его в работу, у Managerа заказ изменит статус на зеленый огонек.
  Когда заказ будет готов статус изменится на красный огонек.Manager нажимает на зеленую машину, заказ отправлен клиенту.
  Ссылка http://Office.somee.com
  
 
