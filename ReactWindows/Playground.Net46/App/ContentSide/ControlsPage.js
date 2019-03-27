import React, { Component } from 'react'
import PropTypes from 'prop-types'
import {
  Text,
  View
} from 'react-native'
import styles from './styles'
import TextInputTest from './TextInputTest'

export default class ControlsPage extends Component {
  static propTypes = {
    logger: PropTypes.func
  }

  constructor(props) {
    super(props)
  }

  render() {
    return (
      <View style={styles.content}>
        <Text>Controls</Text>
        <TextInputTest logger={this.props.logger} />
      </View>
    )
  }
}