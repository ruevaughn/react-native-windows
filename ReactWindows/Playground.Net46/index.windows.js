/**
 * Sample React Native App
 * https://github.com/facebook/react-native
 */

import React, { Component } from 'react';
import {
  AppRegistry,
  StyleSheet,
  View
} from 'react-native';
import MenuSide from './App/MenuSide'
import LogArea from './App/LogArea'
import { Pages, ControlsPage, FixesPage } from './App/ContentSide'
var RCTDeviceEventEmitter = require('RCTDeviceEventEmitter')
import * as Animatable from 'react-native-animatable'

const LOG_INIT_MESSAGE = 'Playground v 0.3'

class Playground extends Component {
  constructor(props) {
    super(props)

    this.state = {
      displayPage: Pages.FIXES,
      log: LOG_INIT_MESSAGE
    }
  }

  switchContent = (page) => {
    if (page === 'CLEAR_LOG') {
      this.setState(previousState => ({
        log: LOG_INIT_MESSAGE
      })
      )
      return
    }

    this.setState(previousState => ({
      displayPage: page,
      log: `${previousState.log}\n${new Date().toISOString()}: Page changed to ${page}`
    }))
  }

  renderContent = () => {
    const { displayPage } = this.state
    return (
      <View style={styles.clientArea}>
        {displayPage === Pages.CONTROLS && <ControlsPage logger={this.log} />}
        {displayPage === Pages.FIXES && <FixesPage logger={this.log} />}
      </View>
    )
  }

  log = (message) => {
    this.setState(previousState => (
      { log: `${previousState.log}\n${new Date().toISOString()}: ${message}` }
    ))
  }

  componentWillMount() {
    RCTDeviceEventEmitter.addListener('logMessageCreated', (evt) => { this.log(`${evt.messageSender}: ${evt.message}`) })
  }

  render() {
    return (
      <View style={styles.container}>
        <Animatable.View style={styles.content} ref='content' animation='fadeInUp' duration={800} easing='ease-in'>
          <View style={styles.content}>
            <MenuSide logger={this.log} menuClick={this.switchContent} />
            {this.renderContent()}
          </View>
        </Animatable.View>
        <LogArea content={this.state.log} />
      </View>
    )
  }
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    flexDirection: 'column',
  },
  content: {
    flex: 1,
    flexGrow: 2,
    flexDirection: 'row',
    justifyContent: 'flex-start',
    alignItems: 'stretch',
    minHeight: 200
  },
  clientArea: {
    flexGrow: 2,
  }
});

AppRegistry.registerComponent('Playground.Net46', () => Playground);
