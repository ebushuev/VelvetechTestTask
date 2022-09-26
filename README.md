Name: Amir
Surname: Ibragimov
Email: ibragimov-amir@list.ru
Phone Number: +992888800080

Get
![image](https://user-images.githubusercontent.com/69799846/192093268-00d37479-c920-429d-b53e-e3caa7b7fec3.png)

GetById
![image](https://user-images.githubusercontent.com/69799846/192093277-06f72f27-b4ce-460d-97e6-676753574b04.png)

Post 
![image](https://user-images.githubusercontent.com/69799846/192093288-85d08f9c-aed6-4cb5-89ac-9ce1577f902b.png)

Put
![image](https://user-images.githubusercontent.com/69799846/192093292-af92d490-916e-4d48-81e9-594396a7acf8.png)

Delete
![image](https://user-images.githubusercontent.com/69799846/192093301-9cb7f0cf-9386-4173-855b-a5c267c4bd81.png)

Logging Error.
saving all logs in TodoListWeb-20220924.txt file
![image](https://user-images.githubusercontent.com/69799846/192093308-ad725536-60b2-4911-9cca-feab30f7ce23.png)

Business Layer(Application) and Data Access Layer.
I usually use that architecture.
![image](https://user-images.githubusercontent.com/69799846/192093323-6c0201e7-ec9a-4f7e-be84-66a9193260cd.png)
![Onion](https://user-images.githubusercontent.com/69799846/192093352-f6553040-4726-4482-8753-64cf7af82cb3.png)

if you can not migrate command just use that:
dotnet ef migrations add TodoList -p .\DataAccessLayer\ -s .\TodoApiDTO\

Start project from TodoApiDTO.
