# CronExpressionParser

Console application that receives one string argument with a six parameter Cron Expression and describes each field into text.

## Instructions

### How to publish the application

```
$ dotnet publish ./src/CronExpressionParser.Core/ --configuration Release --runtime linux-x64 --output publish/linux-x64 --self-contained true
$ dotnet publish ./src/CronExpressionParser.Core/ --configuration Release --runtime osx-x64 --output publish/osx-x64 --self-contained true
$ dotnet publish ./src/CronExpressionParser.Core/ --configuration Release --runtime win-x64 --output publish/win-x64 --self-contained true
```

### How to run the application

```
$ ./publish/linux-x64/CronExpressionParser.Core "*/15 0 1,15 * 1-5 /usr/bin/find"
$ ./publish/osx-x64/CronExpressionParser.Core "*/15 0 1,15 * 1-5 /usr/bin/find"
$ ./publish/win-x64/CronExpressionParser.Core.exe "*/15 0 1,15 * 1-5 /usr/bin/find"
```

### How to run the tests

```
$ cd CronExpressionParser/src/
$ dotnet test
```

## Tooling

- **Visual Studio** 2019
- **.NET** 5.0
- **NUnit** 3.13.2
- **FluentAssertions** 6.2.0
- **GitHub** (for source control)

## References

[cron](https://en.wikipedia.org/wiki/Cron) on Wikipedia

[A Guide To Cron Expressions](https://www.baeldung.com/cron-expressions) on Baeldung
