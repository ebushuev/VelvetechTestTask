# TodoService

Why I added CreateUpdateItemTodoDTO?
- To avoid the scenario when user types different ids in body and route parameter, simply avoiding that

Why didn't I test all the methods in the service?
- methods I didn't test are too simple, thay only use mapper (which is tested) and call repo which calls EF, so it's like testing EF

Possible scaling changes:
1) in case if TodoService will need to talk to more than one repo, it is good to add database service to store all of them
2) if project grows up it's good to separate DAL and BLL to separate projects, like I did with Tests

P.S.
I've passed a real mapper instead of mocked one to service tests, because mapper is already tested and
mocking Automapper makes to sence since its aim is to reduce dumb mapping code
