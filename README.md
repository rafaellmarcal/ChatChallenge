## ChatChallenge

### O Que é?
O Chat Challenge é uma aplicação console desenvolvida utilizando .NET Core (3.1), que possibilita a comunicação entre vários usuários por meio de conexão TCP.

### Objetivo
O objetivo da aplicação é manter uma comunicação simples entre clientes via mensagem de texto,
onde cada usuário ao se conectar deve somente fornercer um nome de usuário e assim iniciar a interação com os demais usuarios já conectados ao servidor.

### Como Executar
* <strong>Visual Studio:</strong><br/>
 1 - Abrir a solution 'ChatChallenge.sln' no Visual Studio;<br/>
 2 - Clicar com o botão direito em 'Solution ChatChallenge' e selecionar a opção 'Properties';<br/>
 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ![image](https://user-images.githubusercontent.com/36934740/128518721-f06ccb59-7247-4e50-ac6a-ac76a78d9911.png) <br/>
 3 - Navegar 'Common Properties' -> 'Startup Project' e selecionar a opção 'Start' para os projetos 'ChatChallenge.ServerSide' e 'ChatChallenge.ClientSide';<br/>
 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ![image](https://user-images.githubusercontent.com/36934740/128518873-165406dc-f386-401a-bd67-7141b91f2a58.png) <br/>
 4 - Manter o projeto 'ChatChallenge.ServerSide' no topo seguido do projeto 'ChatChallenge.ClientSide';<br/>
 5 - Finalizar o startup da solution clicando em 'OK';<br/>
 6 - Iniciar os projetos:<br/>
 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ![image](https://user-images.githubusercontent.com/36934740/128518958-7ed20ca7-27fa-4df9-9dbb-a357131ff721.png) <br/><br/>
 
 * <strong>CMD:</strong><br/>
 1 - Navegar até a pasta onde foi realizada a extração do projeto e que se encontra a solution;<br/>
 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ![image](https://user-images.githubusercontent.com/36934740/128519555-09b012a3-a5ea-40db-b53e-0ce8c775d0c0.png) <br/>
 2 - Executar o comando de build: `dotnet build`;<br/>
 3 - Após conclusão do build, navegar até o diretório: `cd src\ChatChallenge.ServerSide\bin\Debug\netcoreapp3.1`<br/>
 4 - Executar `ChatChallenge.ServerSide.exe`<br/>
 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ![image](https://user-images.githubusercontent.com/36934740/128521398-ac5a95a3-49e4-49b2-9085-454d5db59087.png) <br/>
 5 - E pronto, temos o servidor iniciado;<br/><br/>

 * <strong>Conectando clientes:</strong><br/>
  1 - Para conectarmos mais clientes, basta navergamos até o diretório: `ChatChallenge-master\src\ChatChallenge.ClientSide\bin\Debug\netcoreapp3.1` e executar
  `ChatChallenge.ClientSide.exe`, independente do tipo de inicialização.<br/>
   
  * <strong>Observações:</strong><br/>
  1 - O servidor está apontado para `127.0.0.1` na porta `5200`. Caso a porta já esteja em uso, a mesma pode ser alterada no projeto `ChatChallenge.Resources` classe `ChatChallengeConstants` na propriedade `SERVER_PORT`.<br/>
  2 - No Visual Studio já teremos ao menos um cliente conectado ao iniciar a aplicação.<br/>
