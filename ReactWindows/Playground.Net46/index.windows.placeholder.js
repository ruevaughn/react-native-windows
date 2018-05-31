/**
 * Sample React Native App
 * https://github.com/facebook/react-native
 */

import React, { Component } from 'react';
import {
  AppRegistry,
  StyleSheet,
  Text,
  View,
  TextInput,
  TouchableOpacity
} from 'react-native';

class Playground extends Component {
  constructor(props) {
    super(props)
    this.state = {
      showModal:false
    }
  }

  showModalView = () => {
    return (
      <View style={styles.wrapper}>
        <Text>Modal View</Text>
        <TouchableOpacity style={{height: 20, width:180, backgroundColor:'grey', justifyContent: 'center',alignItems: 'center'}} onPress={() => this.setState({ showModal: false })}><Text>Close</Text></TouchableOpacity>
      </View>
    )
  }

  render() {
    return (
      <View style={styles.container}>
        <Text style={styles.welcome} testID='WelcomeText'>
          Welcome to React Native!
        </Text>
        <Text style={styles.instructions}>
          To get started, edit index.windows.js
        </Text>
        <Text style={styles.instructions}>
          Press Ctrl+R to reload
        </Text>
        <Text style={styles.instructions}>
          Press Ctrl+D or Ctrl+M for dev menu
        </Text>
        <TextInput
                    style={styles.textInput}
                    placeholder = 'Text Input placeholder'
                    placeholderTextColor = 'red'
        />
        {this.state.showModal && this.showModalView()}   
        {!this.state.showModal && <TouchableOpacity style={{height: 20, width:180, backgroundColor:'grey', justifyContent: 'center',alignItems: 'center'}} onPress={() => this.setState({ showModal: true })}><Text style={{color:'white'}}>Show modal view</Text></TouchableOpacity>}
      </View>
    );
  }
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: '#F5FCFF'
  },
  welcome: {
    fontSize: 20,
    textAlign: 'center',
    margin: 10
  },
  instructions: {
    textAlign: 'center',
    color: '#333333',
    marginBottom: 5
  },
  textInput:{
    width:500,
    color: 'black', 
    borderColor: 'transparent',
    borderWidth: 0
  },
  wrapper: {
    position: 'absolute',
    top: 0,
    right: 0,
    bottom: 0,
    left: 0,
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: 'lightgrey',
    width: 900,
    height: 780
  },
});

AppRegistry.registerComponent('Playground.Net46', () => Playground);
