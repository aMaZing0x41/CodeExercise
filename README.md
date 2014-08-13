I have made some assumptions here.

ASSUMPTIONS:
- Assume that sub 5ms per event is fast enough for average processing of events in a "high performance environment".
- Assume that using a 3rd party JSON lib, Newtonsoft.Json is OK.
- Assume that application is a console application.
- Assume all "logging" is done to the console.
- Assume 32 bit build.
- Assume windows environment.

There are 3 projects in the the overall solution.
1. EventMonitor - main application.
2. EventGenerator - testing application used to generate lots of events.
3. EventMonitorTests - unit tests for the application.

Projects are build in VS2012, .net 4.5.