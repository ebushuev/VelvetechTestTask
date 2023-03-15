# TodoService

Слой Application это прикладной уровень, он же Business Layer, он же Use Cases.
Слой Infrastructure включает в себя детали реализации доступа к данным (Data Access Layer).

Так же здесь применен принцип проектирования CQRS. Общепринятой практикой считается что командам (Commands) следует обращаться к данным через репозиторий, извлекая так называемый агрегат. Но в тоже время для запросов(Queries) таких ограничений нет т.к. запросы могут быть сложные, агрегирующие, поэтому для примера я использую DbContext напрямую. Интерфей доступа к агрегату т.е. репозиторий определяться там же где и сам агрегат, в слое предметной логики, но реализуеться в инфраструктурном или слое доступа к данным.
Что бы еще больше развязать слой доступа к данным от предметной логики можно было бы реализовать "модель хранения данных", но для данного примера это было бы переусложнением.
Что бы следовать основной идее CQRS, из действя по созданию TodoItem следую возвращать не польность вновь созданную модель, а только id что бы отдельным запросом клиен мог извлечь данные.
 
В случае ошибки логируется в папку проект TodoApiDTO.Presentation/Logs
