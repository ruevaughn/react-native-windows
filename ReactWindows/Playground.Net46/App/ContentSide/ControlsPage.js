import React, { Component } from 'react'
import {
  Text,
  View,
} from 'react-native';
import styles from './styles'

export default class ControlsPage extends Component {

  render() {
    return (
      <View style={styles.content}>
        <Text>Controls</Text>
      </View>
    )
  }
}