using StudentTerminal.Commands;

#if NEW // Feltölti a JSON file-t új adattal
await StudentCommand.Initialize();
#endif

MenuCommand.Menu();
