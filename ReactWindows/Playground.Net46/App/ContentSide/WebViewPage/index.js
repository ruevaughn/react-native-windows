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

  render() {
    const html = `
    <html>  
    <head>  
        <script type="text/javascript">  
            setTimeout(function () {
              window.external.postMessage("Hello from embedded script")
            }, 2000)
         </script>  
    </head>  
    <body>  
    <div id="outputID" style="color:Red; font-size:16">  
        Hello from HTML document with script!  
    </div>  
    </body>  
    </html>  
    `;

    const script1 = `
      document.body.style.backgroundColor = 'blue';
      true;
    `;

    const script2 = `window.external.PostMessage("Hello from externally injected script") `;

    setTimeout(() => {
      if(this.webref != null) {
        this.webref.injectJavaScript(script2);
      }
    }, 5000);

    return (
      <View style={{ flex: 1 }}>
        <WebView
          injectedJavaScript={script1}
          javaScriptEnabled={true}
          ref={r => (this.webref = r)}
          source={{ html }}
          onLoad={(event) => {
            var jsonString = JSON.stringify(event.nativeEvent, null, 4)
            this.props.logger(`onLoad(${jsonString})\n`)
          }}
          onLoadStart={event => {
            var jsonString = JSON.stringify(event.nativeEvent, null, 4)
            this.props.logger(`onLoadStart(${jsonString})\n`)
          }}   
          onLoadEnd={event => {
            var jsonString = JSON.stringify(event.nativeEvent, null, 4)
            this.props.logger(`onLoadEnd(${jsonString})\n`)
          }}                     
          onMessage={event => {
            this.props.logger(`onMessage(${event.nativeEvent.data})`)
          }}

          onContentSizeChange={event => {
            this.props.logger(`onContentSizeChange(${JSON.stringify(event.nativeEvent)}`)
          }}             
        />
      </View>
    );
  }
}
