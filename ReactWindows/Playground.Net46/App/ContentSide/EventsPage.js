import React, { Component } from 'react'
import {
  Text,
  View,
} from 'react-native';
import styles from './styles'

export default class EventsPage extends Component {

  render() {
    return (
      <View style={styles.content}>
        <Text>Events</Text>
      </View>
    )
  }
}