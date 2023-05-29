dotnet sonarscanner begin /o:"aitanaceca-1" /k:"aitanaceca_Educatan" /d:sonar.host.url="https://sonarcloud.io" /d:sonar.login="978d57318b--------------------3a0fd708c46cc"
dotnet build
dotnet sonarscanner end /d:sonar.login="978d57318b--------------------3a0fd708c46cc"