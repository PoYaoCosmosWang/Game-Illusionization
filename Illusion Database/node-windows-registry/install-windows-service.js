var Service = require('node-windows').Service;

// Create a new service object
var svc = new Service({
    name:'Illusion Database',
    description: 'Illusion Database UI',
    script: 'C:\\Users\\ntuHC\\Desktop\\IllusionDatabase\\app.js'
});

// Listen for the "install" event, which indicates the
// process is available as a service.
svc.on('install',function(){
    console.log('Install complete.');
    svc.start();
});

svc.install();