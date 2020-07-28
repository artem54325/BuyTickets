# BuyTickets

Данное решение состоит из 4 проектов:
1. BuyingTicketCore - asp.net mvc core - для отображения методов используется swagger. БД - postgreSQL
2. XUnitTest - Тестирует проект выше для проверки правильности работы
3. UserApplication - wpf приложение для создания новых сеансов и покупки билетов
4. ClickLikeVk - asp.net mvc core - проект для отслеживания лайков. БД - postgreSQL

Настройки бд пишется в Startup

Для работы UserApplication должен работать BuyingTicketCore и правильно указана ссылка на сервер в /helpers/Request
