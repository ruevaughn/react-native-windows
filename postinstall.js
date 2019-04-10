var fs = require('fs');
var path = require('path');

function copyFileSync( source, target ) {

    var targetFile = target;

    //if target is a directory a new file with the same name will be created
    if ( fs.existsSync( target ) ) {
        if ( fs.lstatSync( target ).isDirectory() ) {
            targetFile = path.join( target, path.basename( source ) );
        }
    }

    fs.writeFileSync(targetFile, fs.readFileSync(source));
}

function copyFolderRecursiveSync( source, target ) {
    var files = [];

    //check if folder needs to be created or integrated
    var targetFolder = path.join( target, path.basename( source ) );
    if ( !fs.existsSync( targetFolder ) ) {
        fs.mkdirSync( targetFolder );
    }

    //copy
    if ( fs.lstatSync( source ).isDirectory() ) {
        files = fs.readdirSync( source );
        files.forEach( function ( file ) {
            var curSource = path.join( source, file );
            if ( fs.lstatSync( curSource ).isDirectory() ) {
                copyFolderRecursiveSync( curSource, targetFolder );
            } else {
                copyFileSync( curSource, targetFolder );
            }
        } );
    }
}

function run() {
  const cwd = process.cwd()
  const libName = 'Libraries'
  console.log('Perform postinstall action ...')
  const src = path.join(cwd, libName)
  const dst = path.join(cwd, '../../react-native')
  console.log(`Copying
from ${src}
to ${dst}`)
  try {
    copyFolderRecursiveSync(src, dst)
    console.log('Done!')
  } catch (e) {
    console.error(`Wasn\'t able to complete postinstall, please copy ${src} to ${dst} manually.`)
  }
}

run()