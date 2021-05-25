/**
 * Sample React Native App
 * https://github.com/facebook/react-native
 */

import React, { Component } from 'react';
import {
  AppRegistry,
  StyleSheet,
  View,
  Text,
  TouchableOpacity,
  Button
} from 'react-native';
import MenuSide from './App/MenuSide'
import LogArea from './App/LogArea'
import { Pages, ControlsPage, FixesPage, MainPage, AccessibilityPage, WebViewPage } from './App/ContentSide'
var RCTDeviceEventEmitter = require('RCTDeviceEventEmitter')
import * as Animatable from 'react-native-animatable'
import GenericModal from "./App/Modals/GenericModal";

const LOG_INIT_MESSAGE = 'Playground v 0.3'

class Playground extends Component {
  constructor(props) {
    super(props)

    this.state = {
      displayPage: Pages.MAIN,
      log: LOG_INIT_MESSAGE,
      isModalOpen: false
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
        {displayPage === Pages.MAIN && <MainPage/>}
        {displayPage === Pages.CONTROLS && <ControlsPage logger={this.log} />}
        {displayPage === Pages.FIXES && <FixesPage logger={this.log} />}
        {displayPage === Pages.ACCESSIBILITY && <AccessibilityPage isFocusable={this.state.isModalOpen === false} logger={this.log} />}
        {displayPage === Pages.WEBVIEW && <WebViewPage isFocusable={this.state.isModalOpen === false} logger={this.log} />}
      </View>
    )
  }

  log = (message) => {
    this.setState(previousState => (
      { log: `${previousState.log}\n${new Date().toISOString()}: ${message}` }
    ))
  }

  modalButtonClickHandler = (isOpen) => {
    this.setState({isModalOpen: isOpen})
  }

  componentWillMount() {
    RCTDeviceEventEmitter.addListener('logMessageCreated', (evt) => { this.log(`${evt.messageSender}: ${evt.message}`) })
  }

  render() {
    return (
      <View isFocusable={this.state.isModalOpen === false} style={styles.container}>
        <Animatable.View isFocusable={this.state.isModalOpen === false} style={styles.content} ref='content' animation='fadeInUp' duration={800} easing='ease-in'>
          <View isFocusable={this.state.isModalOpen === false} style={styles.content}>
            <MenuSide isFocusable={this.state.isModalOpen === false} logger={this.log} menuClick={this.switchContent} />
            {this.renderContent()}
          </View>
        </Animatable.View>
        <LogArea content={this.state.log} />
        <View style={{backgroundColor: 'gray', alignItems: 'center', justifyContent: 'center'}} isFocusable={this.state.isModalOpen === false}>
          <TouchableOpacity onPress={() => this.modalButtonClickHandler(true)}>
            <Text>Show Modal</Text>
          </TouchableOpacity>
        </View>
        <GenericModal isOpen={this.state.isModalOpen} close={() => this.modalButtonClickHandler(false)} />
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
