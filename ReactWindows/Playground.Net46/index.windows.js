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
import { Pages, ControlsPage, EventsPage } from './App/ContentSide'

class Playground extends Component {
  constructor(props) {
    super(props)

    this.state = {
      displayPage: Pages.CONTROLS,
      log: 'Playground v 0.1'
    }
  }

  switchContent = (page) => {
    this.setState( previousState => ({
      displayPage: page,
      log: `${previousState.log}\n${new Date().toISOString()}: Page changed to ${page}`
    }))
  }

  renderContent = () => {
    const { displayPage } = this.state
    return (
      <View style={styles.clientArea}>
        { displayPage === Pages.CONTROLS && <ControlsPage logger={this.log} /> }
        { displayPage === Pages.EVENTS && <EventsPage /> }
      </View>
    )
  }

  log = (message) => {
    this.setState( previousState => (
      {log: `${previousState.log}\n${new Date().toISOString()}: ${message}`}
    ))
  }


  render() {
    return (
      <View style={styles.container}>
        <View style={styles.content}>
          <MenuSide logger={this.log} menuClick={this.switchContent}/>
          {this.renderContent()}
        </View>
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
