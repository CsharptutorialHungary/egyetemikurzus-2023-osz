using StudentTerminal.Commands;

#if NEW // Feltölti a JSON file-t új randomizált adatokkal
await StudentCommand.Initialize();
#endif

await MenuCommand.Menu();