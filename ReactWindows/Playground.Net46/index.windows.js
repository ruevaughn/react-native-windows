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
  Dimensions
} from 'react-native';

class Playground extends Component {
  render() {
    return (
      <View style={styles.container}>
        <View style={styles.left}>
          <View style={styles.title}>
            <Text>My Title</Text>
            <Text>1:59 PM</Text>
          </View>
          <View style={{width: 50, height: 50, backgroundColor: '#ff0000'}} />
        </View>
        <View style={styles.center}>
          <View style={{width: 50, height: 50, backgroundColor: '#ff0000', paddingRight: 10}} />
          <View style={{width: 50, height: 50, backgroundColor: '#00ff00', paddingRight: 10}} />
          <View style={{width: 50, height: 50, backgroundColor: '#0000ff', paddingRight: 10}} />
        </View>
        <View style={styles.right} />
      </View>
    );
  }
}

const styles = StyleSheet.create({
  container: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    width: Dimensions.get('window').width,
    height: 100,
    paddingTop: 20
  },
  left: {
    flexDirection: 'row'
  },
  center: {
    flexDirection: 'row'
  },
  right: {
    width: 350
  },
  title: {
    width: 130,
    marginTop: 7,
    marginLeft: 24,
    marginRight: 10
  }
});

AppRegistry.registerComponent('Playground.Net46', () => Playground);
