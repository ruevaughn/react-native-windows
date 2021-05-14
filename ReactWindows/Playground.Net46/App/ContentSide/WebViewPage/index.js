import React, { Component } from 'react'
import PropTypes from 'prop-types'
import styles from './styles'
import {
  Text,
  View,
  WebView,
  TouchableOpacity
} from 'react-native';

const createFocusableComponent = require('FocusableWindows');
const FocusableTouchableOpacity = createFocusableComponent(TouchableOpacity);

export default class WebViewPage extends Component {
  static propTypes = {
    isFocusable: PropTypes.bool,
    logger: PropTypes.func
  }
  
  componentDidMount() {
	  if(this.refs.focusableButton){
		  this.refs.focusableButton.focus()
	  }
  }

  constructor(props) {
    super(props)
  }

  // render() {
  //   return (
  //     <View style={styles.content}>
  //       <WebView source={{ uri: 'https://sfbay.craigslist.org/' }} />
  //     </View>
  //   )
  // }

  // render() {
  //   const html = `
  //     <html>
  //     <head>Hello World</head>
  //     <body>
  //       <script>
  //         setTimeout(function () {
  //           window.external.PostMessage("Hello!")
  //         }, 2000)
  //       </script>
  //     </body>
  //     </html>
  //   `;

  //   return (
  //     <View style={styles.content}>
  //       <WebView
  //         injectJavaScript={'setTimeout(function () { window.external.PostMessage("Injected Hello!") }, 2000)'}
  //         messagingEnabled={true}
  //         source={{ html }}
  //         onMessage={event => {
  //           this.props.logger(event.nativeEvent.data)
  //           alert(event.nativeEvent.data);
  //         }}
  //         onLoadingStart={event => {
  //           var jsonString = JSON.stringify(event, null, 4)
  //           this.props.logger(`EVENT: onLoadingStart ${jsonString}\n`)
  //         }}
  //         onLoadingFinish={event => {
  //           var jsonString = JSON.stringify(event, null, 4)
  //           this.props.logger(`EVENT: onLoadingFinish ${jsonString}\n`)
  //         }}
  //         updateNavigationState={event => {
  //           var jsonString = JSON.stringify(event, null, 4)
  //           this.props.logger(`EVENT: updateNavigationState ${jsonString}\n`)
  //         }}          
  //       />
  //     </View>
  //   );
  // }

  render() {
    const html = `
    <html>  
    <head>  
        <script type="text/javascript">  
            // Function Without Parameters  
            function JavaScriptFunctionWithoutParameters()    
            {  
              window.external.PostMessage("JavaScriptFunctionWithoutParameters!") 
            }  
 
        </script>  
    </head>  
    <body>  
    <div id="outputID" style="color:Red; font-size:16">  
        Hello from HTML document with script!  
    </div>  
    </body>  
</html>  
    `;
    // function JavaScriptFunctionWithParameters(it)    
    // {  
    //   window.external.PostMessage("JavaScriptFunctionWithParameters[${it}]") 
    // }
    const run = `
      document.body.style.backgroundColor = 'blue';
      true;
    `;

const script2 = `window.external.PostMessage("JavaScriptFunctionWithoutParameters!") `;

    setTimeout(() => {
      this.webref.injectJavaScript(script2);
    }, 3000);
    // injectedJavaScript={'JavaScriptFunctionWithoutParameters'}
    return (
      <View style={{ flex: 1 }}>
        <WebView
        javaScriptEnabled={true}
        injectedJavaScript={run}
          messagingEnabled={true}
          ref={r => (this.webref = r)}
          source={{ html }}
          onMessage={event => {
            this.props.logger(event.nativeEvent.data)
            // alert(event.nativeEvent.data);
          }}
                
        />
      </View>
    );
  }
}

// onLoadStart={event => {
//   var jsonString = JSON.stringify(event, null, 4)
//   this.props.logger(`EVENT: onLoadStart ${jsonString}\n`)
// }}   
// onLoadEnd={event => {
//   var jsonString = JSON.stringify(event, null, 4)
//   this.props.logger(`EVENT: onLoadEnd ${jsonString}\n`)
// }}  
